using Newtonsoft.Json;

namespace TerraformIoCtl.WorkspaceModel
{
  /// <summary>
  /// Workspace
  ///  - Generated - https://app.quicktype.io/#l=cs&r=json2csharp , http://json2csharp.com/
  /// </summary>
  public class Workspace
  {
    [JsonProperty("data")]
    public Data Data { get; set; }

    public static Workspace FromJson(string json) => JsonConvert.DeserializeObject<Workspace>(json, Converter.Settings);
  }

  public static class Serialize
  {
    public static string ToJson(this Workspace self) => JsonConvert.SerializeObject(self, Converter.Settings);
  }
}
