using Newtonsoft.Json;

// var variable = Variable.FromJson(jsonString);
namespace TerraformIoCtl.VariableModel
{
  public class WorkspaceData
  {
    [JsonProperty("id")]
    public string Id { get; set; }

    [JsonProperty("type")]
    public string Type { get; set; } = "workspaces";
  }
}
