using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;

public class Reservation
{
    public Guid Id { get; set; }
    public Guid PropertyId { get; set; }
    public Guid RoomTypeId { get; set; }
    public DateTime ArrivalDate { get; set; }
    public DateTime DepartureDate { get; set; }
    public TimeSpan ArrivalTime { get; set; }
    public TimeSpan DepartureTime { get; set; }
    public string GuestName { get; set; } = null!;
    public string GuestPhoneNumber { get; set; } = null!;
    public decimal Total { get; set; }
    public string Currency { get; set; } = null!;
    public bool IsCanceled { get; set; }
    public Property? Property { get; set; }
    public RoomType? RoomType { get; set; }
}