using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SolutionBuilder
{
  /// <summary>
  /// Interface for Child relations
  /// </summary>
  /// <typeparam name="PType">Paernt</typeparam>
  public interface IChildItem<PType> where PType : class
  {
    /// <summary>
    /// Parent instance
    /// </summary>
    PType Parent { get; set; }

    /// <summary>
    /// Set the parent
    /// </summary>
    /// <param name="parent">Parent</param>
    void SetParent(PType parent);
  }
}
