using System;
using System.Diagnostics;

namespace TerraformIoUtility
{
  [DebuggerDisplay("{Name}, {Value}, {IsSensitive}, {IsHcl}, {Created}")]
  public class Variable
  {
    public string Name { get; set; }
    public string Value { get; set; }
    public bool IsSensitive { get; set; }
    public bool IsHcl { get; set; }
    public DateTime Created { get; set; }
  }
}
