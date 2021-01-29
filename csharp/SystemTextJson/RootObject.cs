using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace SystemTextJson
{
    public class RootObject<T> where T : class, new()
    {
        [JsonPropertyName("count")]
        public int Count {get; set;}
        [JsonPropertyName("next")]
        public string Next {get; set;}
        [JsonPropertyName("previous")]
        public string Previous {get; set;}
        [JsonPropertyName("results")]
        public T[] Results {get; set;}
    }
}