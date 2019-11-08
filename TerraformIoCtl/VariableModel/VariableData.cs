using Newtonsoft.Json;

namespace TerraformIoCtl.VariableModel
{
  public class VariableData
  {
    [JsonProperty("type")]
    public string Type { get; set; } = "vars";

    [JsonProperty("attributes")]
    public Attributes Attributes { get; set; } = new Attributes();

    [JsonProperty("relationships")]
    public Relationships Relationships { get; set; } = new Relationships();
  }
}
