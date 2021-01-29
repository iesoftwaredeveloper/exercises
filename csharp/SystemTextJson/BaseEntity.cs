using System.Text.Json.Serialization;
namespace SystemTextJson
{
    public class BaseEntity
    {
        [JsonPropertyName("id")]
        public virtual uint Id { get; set; }
    }
}