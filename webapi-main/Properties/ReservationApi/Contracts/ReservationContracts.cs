using Infrastructure.Repositories;
using Domain.Shared;

namespace ReservationApi.Contracts;

public record CreateReservationRequest(
    Guid RoomTypeId,
    DateTime ArrivalDate,
    DateTime DepartureDate,
    TimeSpan ArrivalTime,
    TimeSpan DepartureTime,
    string GuestName,
    string GuestPhoneNumber );

public record ReservationResponse(
    Guid Id,
    Guid PropertyId,
    Guid RoomTypeId,
    DateTime ArrivalDate,
    DateTime DepartureDate,
    TimeSpan ArrivalTime,
    TimeSpan DepartureTime,
    string GuestName,
    string GuestPhoneNumber,
    decimal Total,
    string Currency,
    string Status );

public record SearchRequest(
    string City,
    DateTime ArrivalDate,
    DateTime DepartureDate,
    int Guests );

public record SearchResult(
    PropertyResponse Property,
    List<RoomTypeAvailability> AvailableRoomTypes );

public record RoomTypeAvailability(
    Guid RoomTypeId,
    string Name,
    decimal PricePerNight,
    string Currency );

