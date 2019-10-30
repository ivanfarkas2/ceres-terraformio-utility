using Newtonsoft.Json;

// var variable = Variable.FromJson(jsonString);
namespace TerraformIoUtility.VariableModel
{
  public class Workspace
  {
    [JsonProperty("data")]
    public WorkspaceData Data { get; set; } = new WorkspaceData();
  }
}
