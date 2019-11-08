using Newtonsoft.Json;

// var variable = Variable.FromJson(jsonString);
namespace TerraformIoCtl.VariableModel
{
  public class Relationships
  {
    [JsonProperty("workspace")]
    public Workspace Workspace { get; set; } = new Workspace();
  }
}
