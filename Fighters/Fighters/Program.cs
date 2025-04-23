using System.Text;


public class Program
{
    public static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        var menu = new ConsoleMenu();
        var fighterFactory = InitializeFighterFactory( menu );

        var fighters = CreateFighters( fighterFactory );
        PrintFightersInfo( fighters );

        new GameMaster().PlayAndGetWinner( fighters );
    }

    private static IFighterFactory InitializeFighterFactory( ISelectionMenu menu )
    {
        return new FighterFactory(
            new RaceFactory( menu ),
            new WeaponFactory( menu ),
            new ArmorFactory( menu ),
            new ClassesFactory( menu ),
            menu
        );
    }

    private static List<IFighter> CreateFighters( IFighterFactory factory )
    {
        Console.Write( "Введите количество бойцов (минимум 2): " );
        int count = GetValidNumber( 2 );

        var fighters = new List<IFighter>();
        for ( int i = 0; i < count; i++ )
        {
            Console.WriteLine( $"\nСоздание бойца {i + 1}:" );
            fighters.Add( factory.CreateFighter( AskForRandomCreation() ) );
        }
        return fighters;
    }

    private static bool AskForRandomCreation()
    {
        Console.Write( "Создать случайного бойца? (y/n): " );
        return Console.ReadLine()?.ToLower() == "y";
    }

    private static void PrintFightersInfo( IEnumerable<IFighter> fighters )
    {
        Console.WriteLine( "\nСписок бойцов:" );
        foreach ( var fighter in fighters )
        {
            Console.WriteLine( fighter.GetFullInfo() );
        }
    }

    private static int GetValidNumber( int minValue )
    {
        int number;
        while ( !int.TryParse( Console.ReadLine(), out number ) || number < minValue )
        {
            Console.Write( $"Пожалуйста, введите число не меньше {minValue}: " );
        }
        return number;
    }
}

