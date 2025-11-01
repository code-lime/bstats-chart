using System.Text.Json;
using System.Text.Json.Serialization;

namespace DotNet.GitHubAction.Converters;

public abstract class StringJsonConverter<T> : JsonConverter<T>
{
    public abstract T Parse(string value);
    public abstract string ToString(T value);

    public sealed override T ReadAsPropertyName(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Read(ref reader, typeToConvert, options);
    public sealed override void WriteAsPropertyName(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => writer.WritePropertyName(ToString(value));

    public sealed override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        => Parse(reader.GetString() ?? throw new JsonException("String only support"));
    public sealed override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        => writer.WriteStringValue(ToString(value));
}
