using Newtonsoft.Json;

// var variable = Variable.FromJson(jsonString);
namespace TerraformIoCtl.VariableModel
{
  public class Variable
  {
    public Variable() { }

    public Variable(Attributes attributes, string id)
    {
      Data.Attributes = attributes;
      Data.Relationships.Workspace.Data.Id = id;

      if (attributes.Sensitive)
      {
        attributes.Sensitive = false;
        attributes.Value = "??? - Sensitive";
      }
    }

    [JsonProperty("data")]
    public VariableData Data { get; set; } = new VariableData();

    public static Variable FromJson(string json) => JsonConvert.DeserializeObject<Variable>(json, Converter.Settings);
  }

  public static class Serialize
  {
    public static string ToJson(this Variable self) => JsonConvert.SerializeObject(self, Converter.Settings);
  }
}

