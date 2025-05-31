using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationRepository
{
    Task<Reservation?> GetByIdAsync( Guid id );
    Task<List<Reservation>> GetFilteredAsync( ReservationFilter filter );
    Task AddAsync( Reservation reservation );
    Task CancelAsync( Guid id );
    Task<bool> IsRoomTypeAvailableAsync( Guid roomTypeId, DateTime start, DateTime end );
}