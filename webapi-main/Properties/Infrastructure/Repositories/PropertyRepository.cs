using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public class PropertyRepository : IPropertyRepository
{
    private readonly AppDbContext _context;
    public PropertyRepository( AppDbContext context )
    {
        _context = context ?? throw new ArgumentNullException( nameof( context ) );
    }
    public async Task<Property?> GetByIdAsync( Guid id )
        => await _context.Properties
            .Include( p => p.RoomTypes )
            .FirstOrDefaultAsync( p => p.Id == id );

    public async Task<List<Property>> GetAllAsync()
        => await _context.Properties.ToListAsync();

    public async Task AddAsync( Property property )
    {
        await _context.Properties.AddAsync( property );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( Property property )
    {
        _context.Properties.Update( property );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync( Guid id )
    {
        var property = await GetByIdAsync( id );
        if ( property != null )
        {
            _context.Properties.Remove( property );
            await _context.SaveChangesAsync();
        }
    }

    public async Task<List<Property>> GetByCityAsync( string city )
        => await _context.Properties
            .Where( p => EF.Functions.Like( p.City, city ) )
            .ToListAsync();
}