using System.Diagnostics;

public class RaceFactory : IRaceFactory
{
    private readonly ISelectionMenu _menu;
    private readonly List<IRace> _races = new()
    {
        new Human(),
        new Orc(),
    };

    public RaceFactory( ISelectionMenu menu )
    {
        _menu = menu;
    }

    public IRace Create()
    {
        return _menu.SelectFromCollection(
            _races,
            "Выберите расу:"
        );
    }

    public IRace CreateRandom()
    {
        var random = new Random();
        return _races[ random.Next( _races.Count ) ];
    }
}

public class ClassesFactory : IClassesFactory
{
    private readonly ISelectionMenu _menu;
    private readonly List<IClasses> _classes = new()
    {
        new Rogue(),
        new Warrior(),
        new NoClasses()
    };
    public ClassesFactory( ISelectionMenu menu )
    {
        _menu = menu;
    }
    public IClasses Create()
    {
        return _menu.SelectFromCollection(
            _classes,
            "Выберите класс:"
        );
    }

    public IClasses CreateRandom()
    {
        var random = new Random();
        return _classes[ random.Next( _classes.Count ) ];
    }
}