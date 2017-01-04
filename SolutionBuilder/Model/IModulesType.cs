using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder
{

  /// <summary>
  /// Interface for ModulesType & ControlsType
  /// </summary>
  public interface IModulesType : IChildItem<Installation>
  {
    /// <summary>
    /// Output Path
    /// </summary>
    String Output { get; set; }
  }
}
