using System.Text.Json.Serialization;

namespace PropertiesApi.Contracts;

public record CreatePropertyRequest(
    [property: JsonPropertyName( "name" )] string Name );

public record UpdatePropertyRequest(
    [property: JsonPropertyName( "name" )] string Name );

public record PropertyResponse(
    [property: JsonPropertyName( "id" )] Guid Id,
    [property: JsonPropertyName( "name" )] string Name );