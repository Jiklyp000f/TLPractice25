public class Fighter : IFighter
{
    public int MaxHealth => throw new NotImplementedException();

    public int CurrentHealth => throw new NotImplementedException();

    public int Evasion => throw new NotImplementedException();

    public int Initiative => throw new NotImplementedException();

    public string Name => throw new NotImplementedException();

    public string FullName => throw new NotImplementedException();

    public IWeapon Weapon => throw new NotImplementedException();

    public IRace Race => throw new NotImplementedException();

    public IClasses Classing => throw new NotImplementedException();

    public IArmor Armor => throw new NotImplementedException();

    public int CalculateDamage()
    {
        throw new NotImplementedException();
    }

    public int CalculateProtect()
    {
        throw new NotImplementedException();
    }

    public void TakeDamage( int damage )
    {
        throw new NotImplementedException();
    }

    public string WhoIAm()
    {
        throw new NotImplementedException();
    }
}