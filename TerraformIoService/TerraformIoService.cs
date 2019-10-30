using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using TerraformIoUtility.VariableModel;

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
    private const string _mediaType = "application/vnd.api+json";

    public TerraformIoService(IConfiguration config, HttpClient httpClient)
    {
      _config = config;
      Org = _config["ORG"];
      TeamId = _config["TEAM_ID"];
      TeamToken = _config["T_TOKEN"];
      OrgToken = _config["O_TOKEN"];
      BaseUrl = _config["BaseUrl"];

      _httpClient = SetHttpClient(httpClient);
    }

    public async Task<string> GetTeams() => await GetAsync($"organizations/{Org}/teams");
    public async Task<string> GetTeam(string teamId = null) => await GetAsync("teams/" + (string.IsNullOrEmpty(teamId) ? TeamId : teamId));
    public async Task<string> GetWorkspaces() => await GetAsync($"organizations/{Org}/workspaces");
    public async Task<string> ShowWorkspace(string workspaceName) => await GetAsync($"organizations/{Org}/workspaces/{workspaceName}");
    public async Task<string> CloneWorkspace(string workspaceName, string filePath) => await PostAsync($"organizations/{Org}/workspaces", File.ReadAllText(filePath));
    public async Task<string> CreateWorkspace(string filePath) => await PostStreamAsync($"organizations/{Org}/workspaces", WorkspaceModel.Workspace.FromJson(File.ReadAllText(filePath)), CancellationToken.None);

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

    public async Task<List<Attributes>> ListVariables(string workspace)
    {
      var jsonText = await GetAsync($"vars?filter%5Borganization%5D%5Bname%5D={Org}&filter%5Bworkspace%5D%5Bname%5D={workspace}");
      var json = JObject.Parse(jsonText);
      var list = json["data"].Children().ToList();

      // serialize JSON results into .NET objects
      var variables = new List<Attributes>();

      foreach (var item in list)
      {
        var attributes = item["attributes"];
        var variable = new Attributes
        {
          Key = attributes["key"].Value<string>(),
          Value = attributes["value"].Value<string>(),
          Hcl = attributes["hcl"].Value<bool>(),
          Sensitive = attributes["sensitive"].Value<bool>(),
          Created = attributes["created-at"].Value<DateTime>()
        };
        variables.Add(variable);
      }
      return variables;
    }

    public async Task<string> CreateVariable(string targetWorkspaceName, string filePath)
    {
      var id = await ShowWorkspaceId(targetWorkspaceName);
      var attributes = Attributes.FromJson(File.ReadAllText(filePath));
      var variable = new Variable(attributes, id);

      return await PostStreamAsync($"vars", variable, CancellationToken.None);
    }

    public async Task<string> CreateVariable(string targetWorkspaceName, Attributes attributes)
    {
      var id = await ShowWorkspaceId(targetWorkspaceName);
      var variable = new Variable(attributes, id);

      return await PostStreamAsync($"vars", variable, CancellationToken.None);
    }

    public async Task<List<Attributes>> CopyVariables(string sourceWorkspaceName, string targetWorkspaceName)
    {
      var attributesList = await ListVariables(sourceWorkspaceName);

      foreach (var attributes in attributesList)
      {
        await CreateVariable(targetWorkspaceName, attributes);
      }
      return attributesList;
    }

    private async Task<string> GetAsync(string urlSuffix)
    {
      Console.WriteLine($"{BaseUrl}{urlSuffix}");
      var response = await _httpClient.GetAsync(urlSuffix);

      response.EnsureSuccessStatusCode();

      var result = await response.Content.ReadAsStringAsync();
      return result;
    }

    private async Task<string> PostAsync(string urlSuffix, string json)
    {
      Console.WriteLine($"{BaseUrl}{urlSuffix}");

      var content = new StringContent(json, Encoding.UTF8, _mediaType);
      var response = await _httpClient.PostAsync(urlSuffix, content);

      response.EnsureSuccessStatusCode();

      var result = await response.Content.ReadAsStringAsync();
      return result;
    }

    private async Task<string> PostStreamAsync(string urlSuffix, object content, CancellationToken cancellationToken)
    {
      var url = $"{BaseUrl}{urlSuffix}";

      Console.WriteLine(url);

      using (var httpClient = new HttpClient())
      {
        SetHttpClient(httpClient);
        using (var request = new HttpRequestMessage(HttpMethod.Post, url))
        {
          using (var httpContent = CreateHttpContent(content))
          {
            request.Content = httpContent;

            using (var response = await httpClient
              .SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken)
              .ConfigureAwait(false))
            {
              response.EnsureSuccessStatusCode();

              var result = await response.Content.ReadAsStringAsync();
              return result;
            }
          }
        }
      }
    }

    private static void SerializeJsonIntoStream(object value, Stream stream)
    {
      using (var sw = new StreamWriter(stream, new UTF8Encoding(false), 1024, true))
      {
        using (var jtw = new JsonTextWriter(sw) { Formatting = Formatting.None })
        {
          var js = new JsonSerializer();

          js.Serialize(jtw, value);
          jtw.Flush();
        }
      }
    }

    private static HttpContent CreateHttpContent(object content)
    {
      var httpContent = default(HttpContent);

      if (content != null)
      {
        var ms = new MemoryStream();

        SerializeJsonIntoStream(content, ms);
        ms.Seek(0, SeekOrigin.Begin);
        httpContent = new StreamContent(ms);
        httpContent.Headers.ContentType = new MediaTypeHeaderValue(_mediaType);
      }
      return httpContent;
    }

    private HttpClient SetHttpClient(HttpClient client)
    {
      client.BaseAddress = new Uri(BaseUrl);
      client.DefaultRequestHeaders.Accept.Add(MediaTypeWithQualityHeaderValue.Parse(_mediaType));
      client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", OrgToken);
      return client;
    }
  }
}
