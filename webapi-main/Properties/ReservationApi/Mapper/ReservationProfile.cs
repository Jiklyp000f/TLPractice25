using AutoMapper;
using Domain.Entities;
using Domain.Shared;
using ReservationApi.Contracts;

namespace ReservationApi.Mapping;

public class ReservationProfile : Profile
{
    public ReservationProfile()
    {
        CreateMap<Property, PropertyResponse>();
        CreateMap<Reservation, ReservationResponse>()
            .ForMember( dest => dest.Status,
                opt => opt.MapFrom( src => src.IsCanceled ? "Canceled" : "Active" ) );
    }
}