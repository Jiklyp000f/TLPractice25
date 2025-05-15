using System.ComponentModel.DataAnnotations;

namespace PropertiesApi.Contracts;

public record PropertyResponse(
    Guid Id,
    string Name,
    string Country,
    string City,
    string Address,
    double Latitude,
    double Longitude );
public record CreatePropertyRequest(
    [Required] string Name,
    [Required] string Country,
    [Required] string City,
    [Required] string Address,
    double Latitude,
    double Longitude );
public record RoomTypeResponse(
    Guid Id,
    string Name,
    decimal DailyPrice,
    string Currency,
    int MinPersonCount,
    int MaxPersonCount );

public record CreateRoomTypeRequest(
    string Name,
    decimal DailyPrice,
    string Currency,
    int MinPersonCount,
    int MaxPersonCount );
