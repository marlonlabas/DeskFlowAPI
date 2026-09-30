using System.Text.Json.Serialization;
using System.Text.Json;
using System.Globalization;

namespace DeskFlowAPI.Converters;

public class DateTimeSemFracaoConverter : JsonConverter<DateTime>
{
    private const string Formato = "dd-MM-yyyyTHH:mm:ss";
    public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        return DateTime.ParseExact(reader.GetString()!, Formato, CultureInfo.InvariantCulture);
    }
    public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Formato, CultureInfo.InvariantCulture));
    }
}