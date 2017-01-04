using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder
{
    public delegate void AppendTextBoxLogDelegate(String value);

    /// <summary>
    /// Installation (root lovel for Modules & Controls)
    /// </summary>
    public class Installation : IChildItem<Configuration>
    {
        private Configuration parent = null;

        #region IChildItem<Configuration>

        [XmlIgnore()]
        public Configuration Parent
        {
            get { return parent; }
            internal set
            {
                if (parent != value)
                {
                    parent = value;
                    if (parent != null)
                    {
                        this.CompleteOutputPath = System.IO.Path.Combine(parent.Output, this.Output ?? this.Source);
                    }
                }
            }
        }
        Configuration IChildItem<Configuration>.Parent
        {
            get { return this.Parent; }
            set { this.Parent = value; }
        }

        internal void SetParent(Configuration parent)
        {
            Parent = parent;
            foreach (ModulesType module in ModuleType.OfType<ModulesType>()) { module.SetParent(this); }
            foreach (ControlsType module in ModuleType.OfType<ControlsType>()) { module.SetParent(this); }
            Database.Parent = this;
        }
        void IChildItem<Configuration>.SetParent(Configuration parent)
        {
            SetParent(parent);
        }

        #endregion IChildItem<Configuration>

        /// <summary>
        /// Name of the Configuration
        /// </summary>
        [XmlAttribute("Name")]
        public String Name { get; set; }
        /// <summary>
        /// Source Folder (Combined with Parent.SourcePath)
        /// </summary>
        [XmlAttribute("DefaultSourcePath")]
        public String Source { get; set; }

        [XmlAttribute("DefaultOutputPath")]
        public String Output { get; set; }

        /// <summary>
        /// Filter for exclusion (divided by a pipe | )
        /// </summary>
        [XmlAttribute("Exclude")]
        public String Exclude { get; set; }
        /// <summary>
        /// The Compression Level, must be between 1 and 9
        /// </summary>
        [XmlAttribute("CompressionLevel")]
        public UInt16 Compression { get; set; }

        [XmlElement("Exports")]
        public String Exports { get; set; }

        /// <summary>
        /// ModulesType & ControlsType
        /// </summary>
        [XmlElement("Modules", typeof(ModulesType))]
        [XmlElement("Controls", typeof(ControlsType))]
        [XmlElement("Interfaces", typeof(InterfacesType))]
        public List<Object> ModuleType { get; set; }

        /// <summary>
        /// Database Scripts
        /// </summary>
        [XmlElement("Database")]
        public DatabaseType Database { get; set; }

        [XmlElement("Deploy")]
        public Deploy[] Deployes { get; set; }

        /// <summary>
        /// List of Modules
        /// </summary>
        [XmlIgnore()]
        public List<ModuleType> Modules
        {
            get
            {
                if (ModuleType == null) { return null; }
                ModulesType modules = ModuleType.OfType<ModulesType>().FirstOrDefault();
                if (modules == null) { return null; }
                return modules.Modules;
            }
        }

        /// <summary>
        /// List of Controls
        /// </summary>
        [XmlIgnore()]
        public List<ModuleType> Controls
        {
            get
            {
                if (ModuleType == null) { return null; }
                ControlsType controls = ModuleType.OfType<ControlsType>().FirstOrDefault();
                if (controls == null) { return null; }
                return controls.Controls;
            }
        }

        [XmlIgnore()]
        public List<ModuleType> Interfaces
        {
            get
            {
                if (ModuleType == null) { return null; }
                InterfacesType interfaces = ModuleType.OfType<InterfacesType>().FirstOrDefault();
                if (interfaces == null) { return null; }
                return interfaces.Interfaces;
            }
        }

        private String[] exclusions = null;
        private String[] exportList = null;

        [XmlIgnore()]
        public Boolean StopCompilationOnError { get; set; }

        [XmlIgnore()]
        public Boolean AllowMulticoreUsage { get; set; }

        /// <summary>
        /// Should compile Selected Modules/Controls?
        /// </summary>
        [XmlIgnore()]
        public Boolean Compile { get; set; }

        [XmlIgnore()]
        public Boolean DeleteLogs { get; set; }

        /// <summary>
        /// Increment the Build version (0.0.&gt;X&lt;.0)
        /// </summary>
        [XmlIgnore()]
        public Boolean IncrementBuild { get; set; }

        /// <summary>
        /// Execute Scripts?
        /// </summary>
        [XmlIgnore()]
        public Boolean ExecuteScripts { get; set; }

        /// <summary>
        /// Create a package from Selected Modules/Controls?
        /// </summary>
        [XmlIgnore()]
        public Boolean CreatePackage { get; set; }

        /// <summary>
        /// Archive the Package?
        /// </summary>
        [XmlIgnore()]
        public Boolean ArchivePackage { get; set; }

        /// <summary>
        /// The Complete OutputPath for current Installation/Build
        /// </summary>
        [XmlIgnore()]
        public String CompleteOutputPath { get; set; }

        /// <summary>
        /// The Exclude field divided.
        /// </summary>
        [XmlIgnore()]
        public String[] Exclusions
        {
            get
            {
                if (exclusions == null && !String.IsNullOrEmpty(Exclude)) { exclusions = Exclude.Split('|'); }
                return exclusions;
            }
        }

        [XmlIgnore()]
        public String[] ExportList
        {
            get
            {
                if (exportList == null) { exportList = String.IsNullOrEmpty(Exports) ? new String[0] : Exports.Split(new[] { '|' }, StringSplitOptions.RemoveEmptyEntries); }
                return exportList;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public Installation()
        {
            this.ModuleType = new List<Object>();
        }

        public Installation(Installation other)
        {
            this.Compression = other.Compression;
            this.Database = other.Database;
            this.Exclude = other.Exclude;
            this.Name = other.Name;
            this.Parent = other.Parent;
            this.Source = other.Source;
            this.ModuleType = other.ModuleType;
            this.SetParent(other.Parent);
        }

        /// <summary>
        /// Event for Appending Text
        /// </summary>
        public event AppendTextBoxLogDelegate AppendTextBoxLog;

        public void OnAppendTextBoxLog(String value)
        {
            if (AppendTextBoxLog != null)
            {
                AppendTextBoxLog(value);
            }
        }
        public void OnAppendTextBoxLog(String value, params Object[] args)
        {
            OnAppendTextBoxLog(String.Format(value, args));
        }
    }

}
