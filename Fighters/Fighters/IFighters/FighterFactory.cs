public class FighterFactory : IFighterFactory
{
    private readonly IRaceFactory _raceFactory;
    private readonly IWeaponFactory _weaponFactory;
    private readonly IArmorFactory _armorFactory;
    private readonly IClassesFactory _classFactory;
    private readonly ISelectionMenu _menu;

    public FighterFactory(
        IRaceFactory raceFactory,
        IWeaponFactory weaponFactory,
        IArmorFactory armorFactory,
        IClassesFactory classFactory,
        ISelectionMenu menu )
    {
        _raceFactory = raceFactory;
        _weaponFactory = weaponFactory;
        _armorFactory = armorFactory;
        _classFactory = classFactory;
        _menu = menu;
    }

    public IFighter CreateFighter( bool random = false )
    {
        if ( random ) return GenerateRandomFighter();

        string name = GetNameFromUser();
        var race = _raceFactory.Create();
        var weapon = _weaponFactory.Create();
        var armor = _armorFactory.Create();
        var fighterClass = _classFactory.Create();

        return new Fighter( name, race, weapon, armor, fighterClass );
    }

    private string GetNameFromUser()
    {
        Console.Write( "Введите имя бойца: " );
        return Console.ReadLine() ?? "Безымянный";
    }

    private IFighter GenerateRandomFighter()
    {
        var random = new Random();
        return new Fighter(
            NameGenerator.Generate(),
            _raceFactory.CreateRandom(),
            _weaponFactory.CreateRandom(),
            _armorFactory.CreateRandom(),
            _classFactory.CreateRandom()
        );
    }
}

