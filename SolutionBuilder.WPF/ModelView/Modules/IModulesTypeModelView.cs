using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder.WPF.ModelView
{
  /// <summary>
  /// Base Interface for the Modules/Controls Type Mode View (used as "Parent" builder without many code)
  /// </summary>
  public interface IModulesTypeModelView
  {
    /// <summary>
    /// Output Path
    /// </summary>
    String Output { get; set; }
    /// <summary>
    /// Installation Parent
    /// </summary>
    InstallationModelView Parent { get; }
    /// <summary>
    /// Check me or the parent, not children
    /// </summary>
    /// <param name="value"></param>
    void Check(Boolean value);
  }
}
