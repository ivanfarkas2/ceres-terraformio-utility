using System;
using System.Diagnostics;

using Newtonsoft.Json;

namespace TerraformIoUtility.VariableModel
{
  [DebuggerDisplay("{Key}, {Value}, {Category}, {Hcl}, {Sensitive}, {Created}")]
  public class Attributes
  {
    [JsonProperty("key")]
    public string Key { get; set; }

    [JsonProperty("value")]
    public string Value { get; set; }

    [JsonProperty("category")]
    public string Category { get; set; } = "terraform";

    [JsonProperty("hcl")]
    public bool Hcl { get; set; }

    [JsonProperty("sensitive")]
    public bool Sensitive { get; set; }

    [JsonIgnore]
    public DateTime Created { get; set; }

    public static Attributes FromJson(string json) => JsonConvert.DeserializeObject<Attributes>(json, Converter.Settings);
  }
}
