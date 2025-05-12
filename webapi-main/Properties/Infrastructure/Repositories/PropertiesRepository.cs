using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories;

public class PropertiesRepository : IPropertiesRepository
{
    private static List<Property> Properties = [];

    public void Add(Property property)
    {
        Properties.Add(property);
    }

    public Property? GetById(Guid id)
    {
        return Properties.FirstOrDefault(p => p.Id == id);
    }

    public List<Property> List()
    {
        return Properties.ToList();
    }

    public void Update(Property property)
    {
        Property? existingProperty = GetById(property.Id);

        if (existingProperty is null)
        {
            throw new InvalidOperationException($"Property with id {property.Id} does not exist");
        }

        existingProperty.Name = property.Name;
    }

    public void DeleteById(Guid id)
    {
        Property? existingProperty = GetById(id);

        if (existingProperty is null)
        {
            throw new InvalidOperationException($"Property with id {id} does not exist");
        }

        Properties.Remove(existingProperty);
    }
}
