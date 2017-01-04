using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder
{

    /// <summary>
    /// Class that represent Modules & Controls (basic type is exactly the same)
    /// </summary>
    public class ModuleType : IChildItem<Object>
    {
        #region IChildItem<Object>

        [XmlIgnore()]
        public Object Parent
        {
            get;
            internal set;
        }
        Object IChildItem<Object>.Parent
        {
            get { return this.Parent; }
            set { this.Parent = value; }
        }

        internal void SetParent(Object parent)
        {
            this.Parent = parent;
            if (parent is ModulesType) { Type = EModuleType.Module; }
            else if (parent is ControlsType) { Type = EModuleType.Control; }
            else { throw new InvalidOperationException("Selected parent is invalid.\nPlease choose right one."); }
            foreach (Project project in Projects) { project.SetParent(this); }
        }
        void IChildItem<Object>.SetParent(Object parent)
        {
            SetParent(parent);
        }

        #endregion IChildItem<Object>

        /// <summary>
        /// Name of Module
        /// </summary>
        [XmlAttribute("Name")]
        public String Name { get; set; }
        /// <summary>
        /// Path to sln
        /// </summary>
        [XmlElement("Path")]
        public String Path { get; set; }

        /// <summary>
        /// Which configuration? (Debug, Release, or any other)
        /// </summary>
        [XmlElement("Configuration")]
        public String Configuration { get; set; }
        /// <summary>
        /// Platform to build
        /// </summary>
        [XmlElement("Platform")]
        public EPlatformType Platform { get; set; }

        /// <summary>
        /// Projects to build contained in sln file
        /// </summary>
        [XmlElement("Project")]
        public List<Project> Projects { get; set; }

        /// <summary>
        /// List of Possible Platforms (from *proj file)
        /// </summary>
        [XmlIgnore()]
        public List<EPlatformType> Platforms { get; set; }

        [XmlIgnore()]
        public EProjectType ProjectType { get; set; }

        /// <summary>
        /// List of Possible Configurations (from * proj file)
        /// </summary>
        [XmlIgnore()]
        public List<String> Configurations { get; set; }
        /// <summary>
        /// Is Currently Checked?
        /// </summary>
        [XmlIgnore()]
        public Boolean IsChecked { get; set; }
        /// <summary>
        /// The Module Type (Control/Module)
        /// </summary>
        [XmlIgnore()]
        public EModuleType Type
        {
            get;
            internal set;
        }

        /// <summary>
        /// Parser for sln file.
        /// </summary>
        [XmlIgnore()]
        public Onion.SolutionParser.Parser.Model.ISolution VSSolution { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ModuleType()
        {
            Configuration = "Debug";
            Platform = EPlatformType.AnyCPU;
            Platforms = new List<EPlatformType>();
            Configurations = new List<String>();
            Projects = new List<Project>();
        }

        /// <summary>
        /// Initialize the Onion Solution Parser
        /// </summary>
        public void Initialize()
        {
            Installation options = ((IModulesType)this.Parent).Parent;
            VSSolution = Onion.SolutionParser.Parser.SolutionParser.Parse(System.IO.Path.Combine(
              options.Parent.Source,
              options.Source,
              this.Path));
        }

        /// <summary>
        /// Returns the specified Visual Studio project by project name, excluding Solution Folders
        /// </summary>
        /// <param name="projectName">Name of the project</param>
        /// <returns></returns>
        public Onion.SolutionParser.Parser.Model.Project FindProject(string projectName)
        {
            if (VSSolution == null || VSSolution.Projects == null) { return null; }

            // Visual Studio projects end with "proj", e.g. csproj, vbproj, dtproj, etc.
            //return solution.Projects.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase) && p.Name.EndsWith("proj", StringComparison.OrdinalIgnoreCase));

            // Solution folders have the same Name == Path, so exclude solution folders from the search
            Onion.SolutionParser.Parser.Model.Project project = VSSolution.Projects.FirstOrDefault(p => p.Name.Equals(projectName, StringComparison.OrdinalIgnoreCase) && p.Name != p.Path);
            string extension = System.IO.Path.GetExtension(project.Path);
            switch (extension)
            {
                case ".csproj": this.ProjectType = EProjectType.CS; break;
                case ".vbproj": this.ProjectType = EProjectType.VB; break;
                case ".vcxproj": this.ProjectType = EProjectType.VCX; break;
                default: this.ProjectType = EProjectType.Unknown; break;
            }
            return project;
        }

        public bool UpdateAssemblyInfo(FileInfo SourceSln, Onion.SolutionParser.Parser.Model.Project onion_proj, bool majorUpdate, bool minorUpdate, bool buildUpdate, bool revisionUpdate, out string log)
        {
            try
            {
                log = string.Empty;
                FileInfo AssemblyInfo = null;
                string RegexFormatReader = "";
                string RegexFormatWriter = "";
                switch (this.ProjectType)
                {
                    case EProjectType.Unknown:
                        log = "Unknown Project Type";
                        return false;
                    case EProjectType.CS:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));
                        RegexFormatReader = "\\[assembly[:]\\s*Assembly(?<File>\\w*)Version\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(?<Build>\\d+)\\.(?<Revision>\\d+)\"\\)\\]";
                        RegexFormatWriter = "[assembly: Assembly{0}Version(\"{1}.{2}.{3}.{4}\")]";
                        break;
                    case EProjectType.VB:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "My Project", "AssemblyInfo.vb"));
                        RegexFormatReader = "<Assembly:\\s*Assembly(?<File>\\w*)Version\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(?<Build>\\d+)\\.(?<Revision>\\d+)\"\\)>\\s*";
                        RegexFormatWriter = "<Assembly: Assembly{0}Version(\"{1}.{2}.{3}.{4}\")> ";
                        break;
                    case EProjectType.VCX:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "AssemblyInfo.cpp"));
                        RegexFormatReader = "\\[assembly:AssemblyVersionAttribute\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(*|(?<Build>\\d+)\\.(?<Revision>\\d+))\"\\)\\];";
                        RegexFormatWriter = "[assembly:AssemblyVersionAttribute(\"{1}.{2}.(*|{3}.{4})\")];";
                        break;

                }

                //AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));

                String Content = System.IO.File.ReadAllText(AssemblyInfo.FullName);
                //Regex regex = new Regex(Compiler.AssemblyInfoPattern, RegexOptions.Compiled | RegexOptions.Multiline);
                Content = Regex.Replace(Content, RegexFormatReader, match =>
                  String.Format(RegexFormatWriter,
                  match.Groups["File"].Value,
                 majorUpdate ? (Int32.Parse(match.Groups["Major"].Value) + 1).ToString() : match.Groups["Major"].Value,
                 minorUpdate ? (Int32.Parse(match.Groups["Minor"].Value) + 1).ToString() : match.Groups["Minor"].Value,
                 buildUpdate ? (Int32.Parse(match.Groups["Build"].Value) + 1).ToString() : match.Groups["Build"].Value,
                 revisionUpdate ? (Int32.Parse(match.Groups["Revision"].Value) + 1).ToString() : match.Groups["Revision"].Value));
                //match.Groups["Minor"].Value,
                //(Int32.Parse(match.Groups["Build"].Value) + 1).ToString(),
                //match.Groups["Revision"].Value));
                File.WriteAllText(AssemblyInfo.FullName, Content);
                return true;
            }
            catch (Exception ex)
            {
                log = ex.ToString();
                return false;
            }
        }

        public bool UpdateAssemblyInfo(FileInfo SourceSln, Onion.SolutionParser.Parser.Model.Project onion_proj, out string log, short major = -1, short minor = -1, short build = -1, short revision = -1)
        {
            try
            {
                log = string.Empty;
                FileInfo AssemblyInfo = null;
                string RegexFormatReader = "";
                string RegexFormatWriter = "";
                switch (this.ProjectType)
                {
                    case EProjectType.Unknown:
                        log = "Unknown Project Type";
                        return false;
                    case EProjectType.CS:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));
                        RegexFormatReader = "\\[assembly[:]\\s*Assembly(?<File>\\w*)Version\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(?<Build>\\d+)\\.(?<Revision>\\d+)\"\\)\\]";
                        RegexFormatWriter = "[assembly: Assembly{0}Version(\"{1}.{2}.{3}.{4}\")]";
                        break;
                    case EProjectType.VB:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "My Project", "AssemblyInfo.vb"));
                        RegexFormatReader = "<Assembly[:]\\s*Assembly(?<File>\\w*)Version\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(?<Build>\\d+)\\.(?<Revision>\\d+)\"\\)>\\s*";
                        RegexFormatWriter = "<Assembly: Assembly{0}Version(\"{1}.{2}.{3}.{4}\")> ";
                        break;
                    case EProjectType.VCX:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "AssemblyInfo.cpp"));
                        RegexFormatReader = "\\[assembly:AssemblyVersionAttribute\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(*|(?<Build>\\d+)\\.(?<Revision>\\d+))\"\\)\\];";
                        RegexFormatWriter = "[assembly:AssemblyVersionAttribute(\"{1}.{2}.(*|{3}.{4})\")];";
                        break;

                }

                //AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));

                String Content = System.IO.File.ReadAllText(AssemblyInfo.FullName);
                //Regex regex = new Regex(RegexFormat, RegexOptions.Compiled | RegexOptions.Multiline);
                Content = Regex.Replace(Content, RegexFormatReader, match => string.Format(RegexFormatWriter,
                    match.Groups["File"].Value,
                    major > -1 ? major.ToString() : match.Groups["Major"].Value,
                    minor > -1 ? minor.ToString() : match.Groups["Minor"].Value,
                    build > -1 ? build.ToString() : match.Groups["Build"].Value,
                    revision > -1 ? revision.ToString() : match.Groups["Revision"].Value));
                //match.Groups["Minor"].Value,
                //(Int32.Parse(match.Groups["Build"].Value) + 1).ToString(),
                //match.Groups["Revision"].Value));
                File.WriteAllText(AssemblyInfo.FullName, Content);
                return true;
            }
            catch (Exception ex)
            {
                log = ex.ToString();
                return false;
            }
        }

        public bool ReadAssemblyInfo(FileInfo SourceSln, Onion.SolutionParser.Parser.Model.Project onion_proj, out short major, out short minor, out short build, out short revision, out string log)
        {
            major = 0;
            minor = 0;
            build = 0;
            revision = 0;
            log = string.Empty;

            try
            {
                String RegexFormat = "";
                FileInfo AssemblyInfo = null;
                switch (this.ProjectType)
                {
                    case EProjectType.Unknown:
                        log = "Unknown Project Type";
                        return false;
                    case EProjectType.CS:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));
                        RegexFormat = "\\[assembly[:]\\s*Assembly(?<File>\\w*)Version\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(?<Build>\\d+)\\.(?<Revision>\\d+)\"\\)\\]";
                        break;
                    case EProjectType.VB:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "My Project", "AssemblyInfo.vb"));
                        RegexFormat = "<Assembly:\\s*Assembly(?<File>\\w*)Version\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(?<Build>\\d+)\\.(?<Revision>\\d+)\"\\)>\\s*";
                        break;
                    case EProjectType.VCX:
                        AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "AssemblyInfo.cpp"));
                        RegexFormat = "\\[assembly:AssemblyVersionAttribute\\(\"(?<Major>\\d+)\\.(?<Minor>\\d+)\\.(*|(?<Build>\\d+)\\.(?<Revision>\\d+))\"\\)\\];";
                        break;

                }

                if (!AssemblyInfo.Exists)
                {
                    log = string.Format("AssemblyInfo '{0}' not found.", AssemblyInfo.FullName);
                    return false;
                }

                //AssemblyInfo = new FileInfo(System.IO.Path.Combine(System.IO.Path.GetDirectoryName(System.IO.Path.Combine(SourceSln.DirectoryName, onion_proj.Path)), "Properties", "AssemblyInfo.cs"));

                String Content = System.IO.File.ReadAllText(AssemblyInfo.FullName);
                Regex regex = new Regex(RegexFormat, RegexOptions.Compiled | RegexOptions.Multiline);
                MatchCollection matches = regex.Matches(Content);
                if (matches != null && matches.Count > 0)
                {
                    Match m = matches[0];
                    major = Convert.ToInt16(m.Groups["Major"].Value);
                    minor = Convert.ToInt16(m.Groups["Minor"].Value);
                    build = Convert.ToInt16(m.Groups["Build"].Value);
                    revision = Convert.ToInt16(m.Groups["Revision"].Value);
                    return true;
                }

                return false;
            }
            catch (Exception ex)
            {
                log = ex.ToString();
                return false;
            }
        }

        public override String ToString()
        {
            return Type.ToString();
        }
    }

    /// <summary>
    /// Enumeration for Module Type
    /// </summary>
    public enum EModuleType
    {
        Module,
        Control,
    }

    /// <summary>
    /// Which type of platform should i use?
    /// </summary>
    public enum EPlatformType : byte
    {
        MixedPlatforms = 0x0,
        AnyCPU = 0x1,
        Arm = 0x2,
        x64 = 0x3,
        x86 = 0x4,
    }

    public enum EProjectType
    {
        Unknown = 0x0,
        CS = 0x1,
        VB = 0x2,
        VCX = 0x3,
    }
}
