using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Contracts;
using Domain.Shared;
using Infrastructure.Repositories;
using AutoMapper;
using ReservationApi.Contracts;

namespace ReservationApi.Controllers;

[ApiController]
[Route( "api/search" )]
public class SearchController : ControllerBase
{
    private readonly IPropertyRepository _propertyRepo;
    private readonly IRoomTypeRepository _roomTypeRepo;
    private readonly IReservationRepository _reservationRepo;
    private IMapper _mapper;

    [HttpGet]
    public async Task<IActionResult> Search( [FromQuery] SearchRequest request )
    {
        // Получение объектов в городе
        var properties = await _propertyRepo.GetByCityAsync( request.City );

        var results = new List<SearchResult>();
        foreach ( var property in properties )
        {
            var availableRooms = new List<RoomTypeAvailability>();
            foreach ( var roomType in property.RoomTypes )
            {
                var isAvailable = await _reservationRepo.IsRoomTypeAvailableAsync(
                    roomType.Id,
                    request.ArrivalDate,
                    request.DepartureDate );

                if ( isAvailable && roomType.MaxPersonCount >= request.Guests )
                {
                    availableRooms.Add( new(
                        roomType.Id,
                        roomType.Name,
                        roomType.DailyPrice,
                        roomType.Currency
                    ) );
                }
            }
            results.Add( new SearchResult(
        _mapper.Map<PropertyResponse>( property ),
        availableRooms ) );
        }
        return Ok( results );
    }
}
