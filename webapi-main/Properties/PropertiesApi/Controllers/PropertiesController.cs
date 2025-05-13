using Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Contracts;

namespace PropertiesApi.Controllers;

/*
create property:

POST   /api/properties + body
GET    /api/properties
GET    /api/properties/{propertyId}
PUT    /api/properties/{propertyId} + body 
DELETE /api/properties/{propertyId}

anti corruption layer
*/

[ApiController]
[Route( "api/properties" )]
public class PropertiesController : ControllerBase
{
    private readonly IPropertiesRepository _propertiesRepository;

    public PropertiesController( IPropertiesRepository propertiesRepository )
    {
        _propertiesRepository = propertiesRepository;
    }

    [HttpPost]
    public IActionResult Create( [FromBody] CreatePropertyRequest createPropertyRequest )
    {
        Domain.Entities.Property property = new( createPropertyRequest.Name );
        _propertiesRepository.Add( property );

        return Created( "", property.Id );
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        List<Domain.Entities.Property> props = _propertiesRepository.List();
        IEnumerable<Property> propertiesResponse = props.Select( p => new Property( p.Id, p.Name ) );

        return Ok( propertiesResponse );
    }

    [HttpGet( "{propertyId:guid}" )]
    public IActionResult Get( [FromRoute] Guid propertyId )
    {
        Domain.Entities.Property property = _propertiesRepository.GetById( propertyId );
        if ( property is null )
        {
            return NotFound();
        }

        Contracts.Property propertyResponse = new Contracts.Property( property.Id, property.Name );

        return Ok( propertyResponse );
    }

    [HttpPut( "{propertyId:guid}" )]
    public IActionResult Update(
        Guid propertyId,
        [FromBody] UpdatePropertyRequest request )
    {
        try
        {
            Domain.Entities.Property existingProperty = _propertiesRepository.GetById( propertyId );
            if ( existingProperty is null )
            {
                return NotFound();
            }

            var updatedDomainProperty = new Domain.Entities.Property( request.Name )
            {
                Id = existingProperty.Id // Сохраняем оригинальный ID
            };

            // 3. Обновляем через репозиторий
            _propertiesRepository.Update( updatedDomainProperty );

            // 4. Маппим к DTO для ответа
            var response = new Contracts.Property(
                updatedDomainProperty.Id,
                updatedDomainProperty.Name );

            return Ok( response );
        }
        catch ( ArgumentException ex )
        {
            return BadRequest( new { Error = ex.Message } );
        }
        catch ( InvalidOperationException ex )
        {
            return Conflict( new { Error = ex.Message } );
        }
    }

    [HttpDelete( "{propertyId:guid}" )]
    public IActionResult Delete( Guid propertyId )
    {
        try
        {
            _propertiesRepository.DeleteById( propertyId );
            return NoContent();
        }
        catch ( InvalidOperationException ex )
        {
            return NotFound( new { Error = ex.Message } );
        }
    }
}
