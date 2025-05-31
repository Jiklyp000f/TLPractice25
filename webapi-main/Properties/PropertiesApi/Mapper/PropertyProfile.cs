using AutoMapper;
using Domain.Entities;
using PropertiesApi.Contracts;

namespace PropertiesApi.Mapping;

public class PropertyProfile : Profile
{
    public PropertyProfile()
    {
        CreateMap<Property, PropertyResponse>();
        CreateMap<CreatePropertyRequest, Property>();
        CreateMap<RoomType, RoomTypeResponse>();
    }
}