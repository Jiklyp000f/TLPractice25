using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertyRepository
{
    Task<Property?> GetByIdAsync( Guid id );
    Task<List<Property>> GetAllAsync();
    Task AddAsync( Property property );
    Task UpdateAsync( Property property );
    Task DeleteAsync( Guid id );
    Task<List<Property>> GetByCityAsync( string city );
}
