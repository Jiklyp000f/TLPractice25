using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts;
using ReservationApi.Contracts;

namespace PropertiesApi.Controllers;

[ApiController]
[Route( "api/properties/{propertyId}/roomtypes" )]
public class RoomTypesController : ControllerBase
{
    private readonly IRoomTypeRepository _repo;
    private readonly IMapper _mapper;

    [HttpPost]
    public async Task<IActionResult> Create(
    Guid propertyId,
    [FromBody] CreateRoomTypeRequest request )
    {
        var roomType = new RoomType
        {
            Id = Guid.NewGuid(),
            PropertyId = propertyId,
            Name = request.Name,
            DailyPrice = request.DailyPrice,
            Currency = request.Currency,
            MinPersonCount = request.MinPersonCount,
            MaxPersonCount = request.MaxPersonCount
        };

        await _repo.AddAsync( roomType );
        return CreatedAtAction( nameof( GetById ), new { id = roomType.Id },
            _mapper.Map<RoomTypeResponse>( roomType ) );
    }

    [HttpGet( "{id}" )]
    public async Task<IActionResult> GetById( Guid id )
    {
        var roomType = await _repo.GetByIdAsync( id );
        return roomType != null
            ? Ok( _mapper.Map<RoomTypeResponse>( roomType ) )
            : NotFound();
    }
}
