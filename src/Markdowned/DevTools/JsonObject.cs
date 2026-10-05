using System.Text;
using System.Text.Json;
using Pure.Primitives.Abstractions.Char;
using Pure.Primitives.Abstractions.String;
using Char = Pure.Primitives.Char.Char;

namespace Markdowned.DevTools;

public sealed record JsonObject : IString
{
    private readonly IEnumerable<KeyValuePair<string, object>> _members;

    public JsonObject(params IEnumerable<KeyValuePair<string, object>> members)
    {
        _members = members;
    }

    public string TextValue
    {
        get
        {
            using MemoryStream stream = new MemoryStream();
            using (Utf8JsonWriter writer = new Utf8JsonWriter(stream))
            {
                writer.WriteStartObject();

                foreach (KeyValuePair<string, object> member in _members)
                {
                    writer.WritePropertyName(member.Key);

                    switch (member.Value)
                    {
                        case string text:
                            writer.WriteStringValue(text);
                            break;
                        case bool flag:
                            writer.WriteBooleanValue(flag);
                            break;
                        case int number:
                            writer.WriteNumberValue(number);
                            break;
                        case double number:
                            writer.WriteNumberValue(number);
                            break;
                        case IString raw:
                            writer.WriteRawValue(raw.TextValue);
                            break;
                        default:
                            throw new ArgumentException(
                                $"Unsupported JSON value: {member.Value.GetType()}"
                            );
                    }
                }

                writer.WriteEndObject();
            }

            return Encoding.UTF8.GetString(stream.ToArray());
        }
    }

    public IEnumerator<IChar> GetEnumerator()
    {
        return TextValue.Select(symbol => new Char(symbol)).Cast<IChar>().GetEnumerator();
    }

    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public override int GetHashCode()
    {
        throw new NotSupportedException();
    }

    public override string ToString()
    {
        throw new NotSupportedException();
    }
}
