using System.Text.Json.Serialization;

namespace Example
{
    using System;

    public partial class Sample
    {
        [JsonPropertyName("executionWindow")]
        public string[] ExecutionWindow { get; set; }

        [JsonPropertyName("jobs")]
        public Job[] Jobs { get; set; }
    }
}