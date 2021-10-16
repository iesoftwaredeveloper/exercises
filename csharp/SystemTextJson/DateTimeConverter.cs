using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace SystemTextJson
{
    public class DateTimeConverter : JsonConverter<DateTime?>
    {
        public override DateTime? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            return reader.GetDateTime();
        }

        public override void Write(
            Utf8JsonWriter writer,
            DateTime? dateTimeValue,
            JsonSerializerOptions options)
        {
            if(String.IsNullOrWhiteSpace(dateTimeValue.ToString()))
            {
                writer.WriteNullValue();
            }
            writer.WriteStringValue(((DateTime)dateTimeValue).ToString("s",CultureInfo.InvariantCulture));
        }
    }
}