using System;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class ReservationRepository : IReservationRepository
{
    private readonly AppDbContext _context;

    public ReservationRepository( AppDbContext context )
    {
        _context = context ?? throw new ArgumentNullException( nameof( context ) );
    }
    public async Task AddAsync( Reservation reservation )
    {
        await _context.Reservations.AddAsync( reservation );
        await _context.SaveChangesAsync();
    }

    public async Task CancelAsync( Guid id )
    {
        var reservation = await GetByIdAsync( id );
        if ( reservation != null )
        {
            reservation.IsCanceled = true;
            await _context.SaveChangesAsync();
        }
    }
    public async Task<Reservation?> GetByIdAsync( Guid id )
        => await _context.Reservations.FindAsync( id );

    public async Task<List<Reservation>> GetFilteredAsync( ReservationFilter filter )
    {
        var query = _context.Reservations.AsQueryable();

        if ( filter.PropertyId.HasValue )
            query = query.Where( r => r.PropertyId == filter.PropertyId );

        if ( filter.RoomTypeId.HasValue )
            query = query.Where( r => r.RoomTypeId == filter.RoomTypeId );

        if ( filter.ArrivalDateFrom.HasValue )
            query = query.Where( r => r.ArrivalDate >= filter.ArrivalDateFrom );

        if ( filter.ArrivalDateTo.HasValue )
            query = query.Where( r => r.ArrivalDate <= filter.ArrivalDateTo );

        if ( !string.IsNullOrEmpty( filter.GuestName ) )
            query = query.Where( r => r.GuestName.Contains( filter.GuestName ) );

        if ( filter.IsCanceled.HasValue )
            query = query.Where( r => r.IsCanceled == filter.IsCanceled );

        return await query
            .Skip( ( filter.PageNumber - 1 ) * filter.PageSize )
            .Take( filter.PageSize )
            .ToListAsync();
    }

    public Task<bool> IsRoomTypeAvailableAsync( Guid roomTypeId, DateTime start, DateTime end )
    {
        throw new NotImplementedException();
    }
}
