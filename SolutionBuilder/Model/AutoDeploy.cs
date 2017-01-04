using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder
{
    /// <summary>
    /// Deploy xml tree
    /// </summary>
    public class Deploy
    {
        /// <summary>
        /// The type of deploy (Development, UAT (pre-prod) or Production)
        /// </summary>
        [XmlAttribute("class")]
        public DeployClass Class { get; set; }

        [XmlElement("AutomaticDeploy")]
        public List<AutoDeploy> AutoDeployes { get; set; }
    }


    public class AutoDeploy
    {
        /// <summary>
        /// Type to deploy: modules or controls...
        /// </summary>
        [XmlAttribute("type")]
        public DeployType Type { get; set; }
        /// <summary>
        /// Machine name
        /// <example>GRRUNMESXX</example>
        /// </summary>
        [XmlElement("machine")]
        public String Machine { get; set; }
        /// <summary>
        /// Complete path for the deploy
        /// <example>E:\...\WebApi\Modules\...</example>
        /// </summary>
        [XmlElement("path")]
        public String Path { get; set; }

        /// <summary>
        /// Backup any previous folder (default: true)
        /// </summary>
        [XmlAttribute("Backup")]
        public Boolean BackupPrevious { get; set; }


        public AutoDeploy()
        {
            BackupPrevious = true;
        }
    }

    public enum DeployClass : byte
    {
        Development = 0,
        UAT,
        Production,
    }
    public enum DeployType
    {
        Modules,
        Controls,
    }
}
