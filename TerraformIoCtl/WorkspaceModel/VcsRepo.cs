using Newtonsoft.Json;

namespace TerraformIoCtl.WorkspaceModel
{
  public class VcsRepo
  {
    [JsonProperty("oauth-token-id")]
    public string OauthTokenId { get; set; }

    [JsonProperty("branch")]
    public string Branch { get; set; }

    [JsonProperty("ingress-submodules")]
    public bool IngressSubmodules { get; set; }

    [JsonProperty("identifier")]
    public string Identifier { get; set; }
  }
}
