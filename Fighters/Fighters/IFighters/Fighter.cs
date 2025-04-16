public class Fighter : IFighter
{
    public int MaxHealth => Race.Health + Weapon.Health + Classes.Health + Armor.Health;

    public int CurrentHealth { get; private set; }

    public int Evasion => Weapon.Evasion + Classes.Evasion + Armor.Evasion;

    public int Initiative => Race.Initiative;

    public string Name { get; }

    public string FullName => $"{Race.Name} {Name} - {Classes.Name}";

    public IWeapon Weapon { get; }

    public IRace Race { get; }

    public IClasses Classes { get; }

    public IArmor Armor { get; }

    public Fighter( string name, IRace race, IWeapon weapon, IArmor armor, IClasses classes )
    {
        Name = name;
        Race = race;
        Weapon = weapon;
        Armor = armor;
        Classes = classes;
        CurrentHealth = MaxHealth;
    }

    public int CalculateDamage()
    {
        return Race.Damage + Classes.Damage + Weapon.Damage;
    }

    public int CalculateProtect()
    {
        return Classes.Armor + Armor.Armor;
    }

    public void TakeDamage( int damage )
    {
        CurrentHealth -= damage;
        if ( CurrentHealth < 0 )
        {
            CurrentHealth = 0;
        }
    }

    public string WhoIAm()
    {
        return "Информация обо мне:\n" + FullName;
    }
}