public interface IFighter
{
    public int MaxHealth { get; }
    public int CurrentHealth { get; }
    public int Evasion { get; }
    public int Initiative { get; }
    public string Name { get; }
    public string FullName { get; }

    public IWeapon Weapon { get; }
    public IRace Race { get; }
    public IClasses Classes { get; }
    public IArmor Armor { get; }

    public void TakeDamage( int damage );
    public int CalculateDamage();
    public int CalculateProtect();
    public string WhoIAm();
}
