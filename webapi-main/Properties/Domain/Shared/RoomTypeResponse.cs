using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared;

public record RoomTypeResponse(
    Guid Id,
    string Name,
    decimal DailyPrice,
    string Currency,
    int MinPersonCount,
    int MaxPersonCount );
