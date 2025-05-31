using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class RoomTypeRepository : IRoomTypeRepository
{
    private readonly AppDbContext _context;

    public RoomTypeRepository( AppDbContext context )
    {
        _context = context ?? throw new ArgumentNullException( nameof( context ) );
    }

    public async Task AddAsync( RoomType roomType )
    {
        await _context.RoomTypes.AddAsync( roomType );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync( Guid id )
    {
        var roomType = await _context.RoomTypes.FindAsync( id );
        if ( roomType != null )
        {
            _context.RoomTypes.Remove( roomType );
            await _context.SaveChangesAsync();
        }
    }

    public async Task<RoomType?> GetByIdAsync( Guid id )
        => await _context.RoomTypes.FindAsync( id );

    public async Task<List<RoomType>> GetByPropertyIdAsync( Guid propertyId )
        => await _context.RoomTypes
            .Where( rt => rt.PropertyId == propertyId )
            .ToListAsync();

    public async Task UpdateAsync( RoomType roomType )
    {
        _context.RoomTypes.Update( roomType );
        await _context.SaveChangesAsync();
    }
}