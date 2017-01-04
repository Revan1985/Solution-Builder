using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SolutionBuilder.Module
{
  public class MergerType
  {
    public MergerType() { }

    [XmlAttribute("Source")]
    public String Source { get; set; }

    [XmlAttribute("Destination")]
    public String Destination { get; set; }
  }
}
