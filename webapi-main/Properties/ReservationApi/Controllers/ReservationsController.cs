using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Contracts;

namespace ReservationApi.Controllers;

[ApiController]
[Route( "api/reservations" )]
public class ReservationsController : ControllerBase
{
    private readonly IReservationRepository _reservationRepo;
    private readonly IRoomTypeRepository _roomTypeRepo;
    private readonly IMapper _mapper;

    public ReservationsController(
        IReservationRepository reservationRepo,
        IRoomTypeRepository roomTypeRepo,
        IMapper mapper )
    {
        _reservationRepo = reservationRepo;
        _roomTypeRepo = roomTypeRepo;
        _mapper = mapper;
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateReservationRequest request )
    {

        var isAvailable = await _reservationRepo.IsRoomTypeAvailableAsync(
        request.RoomTypeId,
        request.ArrivalDate,
        request.DepartureDate );



        if ( !isAvailable )
            return BadRequest( "Room type is not available" );

        var roomType = await _roomTypeRepo.GetByIdAsync( request.RoomTypeId );
        if ( roomType == null )
            return BadRequest( "Invalid Room Type" );

        var nights = ( request.DepartureDate - request.ArrivalDate ).Days;
        var total = roomType.DailyPrice * nights;

        var reservation = new Reservation
        {
            RoomTypeId = request.RoomTypeId,
            PropertyId = roomType.PropertyId,
            ArrivalDate = request.ArrivalDate,
            DepartureDate = request.DepartureDate,
            ArrivalTime = request.ArrivalTime,
            DepartureTime = request.DepartureTime,
            GuestName = request.GuestName,
            GuestPhoneNumber = request.GuestPhoneNumber,
            Total = total,
            Currency = roomType.Currency
        };

        await _reservationRepo.AddAsync( reservation );
        return CreatedAtAction( nameof( GetById ), new { id = reservation.Id },
            _mapper.Map<ReservationResponse>( reservation ) );
    }

    [HttpGet( "{id}" )]
    public async Task<IActionResult> GetById( Guid id )
    {
        var reservation = await _reservationRepo.GetByIdAsync( id );
        return reservation != null
            ? Ok( _mapper.Map<ReservationResponse>( reservation ) )
            : NotFound();
    }

    [HttpGet]
    public async Task<IActionResult> GetFiltered(
    [FromQuery] Guid? propertyId,
    [FromQuery] Guid? roomTypeId,
    [FromQuery] DateTime? arrivalDateFrom,
    [FromQuery] DateTime? arrivalDateTo,
    [FromQuery] string? guestName,
    [FromQuery] bool? isCanceled,
    [FromQuery] int page = 1,
    [FromQuery] int pageSize = 20 )
    {
        var filter = new ReservationFilter
        {
            PropertyId = propertyId,
            RoomTypeId = roomTypeId,
            ArrivalDateFrom = arrivalDateFrom,
            ArrivalDateTo = arrivalDateTo,
            GuestName = guestName,
            IsCanceled = isCanceled,
            PageNumber = page,
            PageSize = pageSize
        };

        var reservations = await _reservationRepo.GetFilteredAsync( filter );
        return Ok( _mapper.Map<List<ReservationResponse>>( reservations ) );
    }

}