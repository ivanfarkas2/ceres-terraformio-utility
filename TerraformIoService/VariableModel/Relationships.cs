using Newtonsoft.Json;

// var variable = Variable.FromJson(jsonString);
namespace TerraformIoUtility.VariableModel
{
  public class Relationships
  {
    [JsonProperty("workspace")]
    public Workspace Workspace { get; set; } = new Workspace();
  }
}
