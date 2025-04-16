public class Program
{
    public static void Main()
    {
        List<IFighter> fighters = new List<IFighter>();

        // Запрашиваем количество бойцов
        Console.WriteLine( "Введите количество бойцов: " );
        int numFighters;
        while ( !int.TryParse( Console.ReadLine(), out numFighters ) || numFighters < 2 )
        {
            Console.WriteLine( "Пожалуйста, введите корректное число (минимум 2): " );
        }

        // Создаем бойцов
        for ( int i = 0; i < numFighters; i++ )
        {
            Console.WriteLine( $"\nСоздание бойца {i + 1}:" );
            fighters.Add( CreateFighter() );
        }

        // Отображаем информацию о бойцах
        Console.WriteLine( "\nБойцы:" );
        foreach ( var fighter in fighters )
        {
            Console.WriteLine( $"{fighter.FullName}" );
        }


        // Начинаем бой
        var master = new GameMaster();
        var winner = master.PlayAndGetWinner( fighters );

        Console.WriteLine( $"\nПобедитель: {winner.Name}" );
    }

    public static IFighter CreateFighter()
    {
        Console.WriteLine( "Введите данные бойца:" );

        string name = GetFighterName();
        IRace race = GetRace();
        IWeapon weapon = GetWeapon();
        IArmor armor = GetArmor();
        IClasses classes = GetClasses();

        return new Fighter( name, race, weapon, armor, classes );
    }

    public static string GetFighterName()
    {
        Console.WriteLine( "Введите имя бойца: " );

        string fighterName = Console.ReadLine();
        if ( string.IsNullOrEmpty( fighterName ) )
        {
            Console.WriteLine( "Некорректное имя, попробуйте снова: " );
            return GetFighterName();
        }

        return fighterName;
    }

    public static IArmor GetArmor()
    {
        Console.WriteLine( "Выберите броню (дерево, кольчуга): " );

        string armor = Console.ReadLine().ToLower();
        if ( string.IsNullOrEmpty( armor ) )
        {
            Console.WriteLine( "Некорректный выбор брони, попробуйте снова: " );
            return GetArmor();
        }
        return GetArmorByType( armor );
    }

    public static IArmor GetArmorByType( string armorType )
    {
        switch ( armorType )
        {
            case "дерево":
                return new WoodArmor();
            case "кольчуга":
                return new ChainArmor();
            default:
                return new NoArmor();
        }
    }

    public static IRace GetRace()
    {
        Console.WriteLine( "Выберите расу (человек, орк): " );

        string race = Console.ReadLine().ToLower();
        if ( string.IsNullOrEmpty( race ) )
        {
            Console.WriteLine( "Некорректный выбор расы, попробуйте снова: " );
            return GetRace();
        }
        return GetRaceByType( race );
    }

    public static IRace GetRaceByType( string raceType )
    {
        switch ( raceType )
        {
            case "человек":
                return new Human();
            case "орк":
                return new Orc();
            default:
                return new Human();
        }
    }

    public static IWeapon GetWeapon()
    {
        Console.WriteLine( "Выберите оружие (нож, меч): " );

        string weapon = Console.ReadLine().ToLower();
        if ( string.IsNullOrEmpty( weapon ) )
        {
            Console.WriteLine( "Некорректный выбор оружия, попробуйте снова: " );
            return GetWeapon();
        }
        return GetWeaponByType( weapon );
    }

    public static IWeapon GetWeaponByType( string weaponType )
    {
        switch ( weaponType )
        {
            case "нож":
                return new Knife();
            case "меч":
                return new Sword();
            default:
                return new NoWeapon();
        }
    }

    public static IClasses GetClasses()
    {
        Console.WriteLine( "Выберите класс (разбойник, воин): " );

        string classes = Console.ReadLine().ToLower();
        if ( string.IsNullOrEmpty( classes ) )
        {
            Console.WriteLine( "Некорректный выбор класса, попробуйте снова: " );
            return GetClasses();
        }
        return GetClassByType( classes );
    }

    public static IClasses GetClassByType( string classType )
    {
        switch ( classType )
        {
            case "разбойник":
                return new Rogue();
            case "воин":
                return new Warrior();
            default:
                return new NoClasses();
        }
    }
}
