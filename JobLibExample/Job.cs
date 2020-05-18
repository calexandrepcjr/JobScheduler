using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace Example
{
    public partial class Job
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("description")]
        public string Description { get; set; }

        [JsonPropertyName("expiresAt")]
        public string ExpiresAt { get; set; }

        [JsonPropertyName("estimatedTime")]
        public string EstimatedTime { get; set; }
    }
}
