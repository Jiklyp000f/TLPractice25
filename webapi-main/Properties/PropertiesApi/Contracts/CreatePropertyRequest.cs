using System.Text.Json.Serialization;

namespace PropertiesApi.Contracts;

public class CreatePropertyRequest
{
    [JsonPropertyName("name")]
    public string Name { get; set; }
}
