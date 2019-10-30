using Newtonsoft.Json;

namespace TerraformIoUtility.WorkspaceModel
{
  public class Data
  {
    [JsonProperty("type")]
    public string Type { get; set; }

    [JsonProperty("attributes")]
    public Attributes Attributes { get; set; }
  }
}
