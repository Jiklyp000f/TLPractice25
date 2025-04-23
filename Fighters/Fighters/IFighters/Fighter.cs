using System.Text;

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
    public string GetHealthDisplay()
    => GetColoredHearts( CurrentHealth, MaxHealth );
    public static string GetColoredHearts( int current, int max, int heartsPerLine = 10 )
    {
        // Рассчитываем ценность одного сердца
        int heartValue = ( int )Math.Ceiling( ( double )max / heartsPerLine );
        int filledHearts = ( int )Math.Ceiling( ( double )current / heartValue );

        // Выбираем цвет в зависимости от процента здоровья
        double percent = ( double )current / max;
        string colorCode = percent > 0.5 ? "\x1b[32m" :  // зеленый
                           percent > 0.2 ? "\x1b[33m" :  // желтый
                           "\x1b[31m";                   // красный

        // Собираем строку с цветными сердцами
        string hearts =
            $"{colorCode}" +
            $"{new string( '♥', filledHearts )}\x1b[0m" +  // цветные заполненные
            $"{new string( '♡', heartsPerLine - filledHearts )}";  // серые пустые

        // Добавляем числовое отображение
        return $"{hearts} {colorCode}{current}\x1b[0m/{max}";
    }

    public string GetFullInfo()
    {
        var sb = new StringBuilder();
        sb.AppendLine( $"Имя: {Name}" );
        sb.AppendLine( $"Раса: {Race.Name}" );
        sb.AppendLine( $"Класс: {Classes.Name}" );
        sb.AppendLine( $"Оружие: {Weapon.Name}" );
        sb.AppendLine( $"Броня: {Armor.Name}" );
        sb.AppendLine( $"Здоровье: {GetHealthDisplay()}" );
        sb.AppendLine( $"Инициатива: \x1b[36m{Initiative}\x1b[0m" );
        return sb.ToString();
    }
}