public class WeaponFactory : IWeaponFactory
{
    private readonly List<IWeapon> _weapons = new()
    {
        new Sword(),
        new Knife(),
        new NoWeapon()
    };

    private readonly ISelectionMenu _menu;

    public WeaponFactory( ISelectionMenu menu )
    {
        _menu = menu;
    }

    public IWeapon Create() => _menu.SelectFromCollection(
        _weapons,
        "Выберите оружие:" );

    public IWeapon CreateRandom()
    {
        var random = new Random();
        return _weapons[ random.Next( _weapons.Count ) ];
    }
}