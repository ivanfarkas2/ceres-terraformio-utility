using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json.Linq;

namespace TerraformIoUtility
{
  public class TerraformIoService
  {
    private readonly string Org;
    private readonly string TeamId;
    private readonly string TeamToken;
    private readonly string OrgToken;
    private readonly string BaseUrl;

    private readonly IConfiguration _config;
    private readonly HttpClient _httpClient;

    public TerraformIoService(IConfiguration config, HttpClient client)
    {
      _httpClient = client;
      _config = config;

      Org = _config["ORG"];
      TeamId = _config["TEAM_ID"];
      TeamToken = _config["T_TOKEN"];
      OrgToken = _config["O_TOKEN"];
      BaseUrl = _config["BaseUrl"];

      client.BaseAddress = new Uri(BaseUrl);
      client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse("application/vnd.api+json; v=2.0"));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OrgToken);
    }

    public async Task<string> GetTeams() => await GetText($"organizations/{Org}/teams");
    public async Task<string> GetTeam(string teamId = null) => await GetText($"teams/{string.IsNullOrEmpty(teamId) : TeamId : teamId}");
    public async Task<string> GetWorkspaces() => await GetText($"organizations/{Org}/workspaces");
    public async Task<string> ShowWorkspace(string workspaceName) => await GetText($"organizations/{Org}/workspaces/{workspaceName}");

    public async Task<string> ShowWorkspaceId(string workspace)
    {
      var text = await ShowWorkspace(workspace);
      var json = JObject.Parse(text);
      var idJson = json["data"].Children().ToList();
      foreach (var item in idJson)
      {
        var jProperty = (JProperty)item;
        var name = jProperty.Name;

        if (name == "id")
        {
          return (string)((JValue)jProperty.Value).Value;
        }
      }
      return null;
    }

    public async Task<List<Variable>> ListVariables(string workspace)
    {
      var jsonText = await GetText($"vars?filter%5Borganization%5D%5Bname%5D={Org}&filter%5Bworkspace%5D%5Bname%5D={workspace}");
      var json = JObject.Parse(jsonText);
      var list = json["data"].Children().ToList();

      // serialize JSON results into .NET objects
      var variables = new List<Variable>();

      foreach (var item in list)
      {
        var attributes = item["attributes"];
        var variable = new Variable
        {
          Name = attributes["key"].Value<string>(),
          Value = attributes["value"].Value<string>(),
          IsHcl = attributes["hcl"].Value<bool>(),
          IsSensitive = attributes["sensitive"].Value<bool>(),
          Created = attributes["created-at"].Value<DateTime>()
        };
        variables.Add(variable);
      }
      return variables;
    }

    private async Task<string> GetText(string urlSuffix)
    {
      Console.WriteLine($"{BaseUrl}{urlSuffix}");
      var response = await _httpClient.GetAsync(urlSuffix);

      response.EnsureSuccessStatusCode();

      var result = await response.Content.ReadAsStringAsync();
      return result;
    }
  }
}
