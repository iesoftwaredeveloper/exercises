using System;
using System.Text.Json.Serialization;

namespace SystemTextJson
{
    public class Result : BaseEntity
    {
        [JsonPropertyName("full_name")]
        public string FullName { get; set; }
        [JsonPropertyName("first_name")]
        public string FirstName { get; set; }
        [JsonPropertyName("last_name")]
        public string LastName { get; set; }
        [JsonPropertyName("company")]
        [JsonIgnore]
        public string Company { get; set; }
        [JsonPropertyName("email")]
        public string Email { get; set; }
        [JsonPropertyName("last_login")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? LastLogin { get; set; }
        [JsonPropertyName("date_joined")]
        public DateTime? DateJoined { get; set; }
        [JsonPropertyName("groups")]
        public uint[] Groups { get; set; }

        [JsonPropertyName("teams")]
        public uint[] Teams { get; set; }

        [JsonPropertyName("member_of")]
        public uint[] MemberOf { get; set; }

    }
}