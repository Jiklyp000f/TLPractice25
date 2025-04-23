internal class NoWeapon : IWeapon
{
    public int Damage => 0;

    public int Evasion => 0;

    public int Health => 0;

    public string Name => "без оружия";
}