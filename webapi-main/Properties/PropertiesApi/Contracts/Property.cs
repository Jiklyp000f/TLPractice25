namespace PropertiesApi.Contracts;

public class Property
{
    public Guid Id { get; }
    public string Name { get; }

    public Property(Guid id, string name)
    {
        Id = id;
        Name = name;
    }
}
