public class ArmorFactory : IArmorFactory
{
    private readonly ISelectionMenu _menu;
    private readonly List<IArmor> _armor = new()
    {
        new WoodArmor(),
        new ChainArmor(),
        new NoArmor()
    };
    public ArmorFactory( ISelectionMenu menu )
    {
        _menu = menu;
    }
    public IArmor Create()
    {
        return _menu.SelectFromCollection(
            _armor,
            "Выберите броню:"
        );
    }

    public IArmor CreateRandom()
    {
        var random = new Random();
        return _armor[ random.Next( _armor.Count ) ];
    }
}