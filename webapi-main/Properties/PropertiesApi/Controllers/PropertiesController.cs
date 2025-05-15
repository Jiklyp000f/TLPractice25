using AutoMapper;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts;

namespace PropertiesApi.Controllers;

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertyRepository _repo;
    private readonly IMapper _mapper;

    public PropertiesController( IPropertyRepository repo, IMapper mapper )
    {
        _repo = repo;
        _mapper = mapper;
    }

    [HttpGet( "{id}" )]
    public async Task<IActionResult> GetById( Guid id )
    {
        var property = await _repo.GetByIdAsync( id );
        return property != null
            ? Ok( _mapper.Map<PropertyResponse>( property ) )
            : NotFound();
    }
}