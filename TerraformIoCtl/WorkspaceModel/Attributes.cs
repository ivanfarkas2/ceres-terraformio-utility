using System.Collections.Generic;

using Newtonsoft.Json;

namespace TerraformIoCtl.WorkspaceModel
{
  public class Attributes
  {
    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("auto-apply")]
    public bool AutoApply { get; set; }

    [JsonProperty("description")]
    public string Description { get; set; }

    [JsonProperty("file-triggers-enabled")]
    public bool FileTriggersEnabled { get; set; }

    [JsonProperty("source-name")]
    public string SourceName { get; set; }

    [JsonProperty("source-url")]
    public object SourceUrl { get; set; }

    [JsonProperty("queue-all-runs")]
    public bool QueueAllRuns { get; set; }

    [JsonProperty("speculative-enabled")]
    public bool SpeculativeEnabled { get; set; }

    [JsonProperty("terraform-version")]
    public string TerraformVersion { get; set; }

    [JsonProperty("trigger-prefixes")]
    public List<object> TriggerPrefixes { get; set; }

    [JsonProperty("working-directory")]
    public string WorkingDirectory { get; set; }

    [JsonProperty("vcs-repo")]
    public VcsRepo VcsRepo { get; set; }
  }
}