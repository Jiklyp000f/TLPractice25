using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Shared;

public record PropertyResponse(
    Guid Id,
    string Name,
    string Country,
    string City,
    string Address,
    double Latitude,
    double Longitude,
    List<RoomTypeResponse> RoomTypes );