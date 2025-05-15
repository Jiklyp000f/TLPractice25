using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypeRepository
{
    Task<RoomType?> GetByIdAsync( Guid id );
    Task<List<RoomType>> GetByPropertyIdAsync( Guid propertyId );
    Task AddAsync( RoomType roomType );
    Task UpdateAsync( RoomType roomType );
    Task DeleteAsync( Guid id );
}
