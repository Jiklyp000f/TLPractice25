using Domain.Entities;

namespace Domain.Repositories;

public interface IPropertiesRepository
{
    void Add(Property property);
    Property? GetById(Guid id);
    List<Property> List();
    void Update(Property property);
    void DeleteById(Guid id);
}
