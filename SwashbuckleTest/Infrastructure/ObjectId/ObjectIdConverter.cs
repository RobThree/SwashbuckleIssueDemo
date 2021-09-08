using System;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace SwashbuckleTest.Infrastructure.ObjectId
{
    public class ObjectIdConverter : JsonConverter<ObjectId>
    {
        public override ObjectId Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
            => reader.TokenType switch
            {
                JsonTokenType.Null => ObjectId.Empty,
                JsonTokenType.String => reader.GetString()!,
                JsonTokenType.Number => reader.GetInt64(),
                _ => ObjectId.Empty,
            };

        public override void Write(Utf8JsonWriter writer, ObjectId value, JsonSerializerOptions options) => writer.WriteStringValue(value);
    }
}
