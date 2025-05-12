namespace Domain.Entities;

public class Property
{
    public Guid Id { get; set; }
    public string Name { get; set; }

    public Property( string name )
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException($"\"{nameof(name)}\" не может быть пустым или содержать только пробел.", nameof(name));
        }

        Id = Guid.NewGuid();
        Name = name;
    }
}
