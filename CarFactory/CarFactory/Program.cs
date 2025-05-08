using System;
class Program
{
    private static List<IEngine> engines = new List<IEngine>
        {
            new GasolineEngine(),
            new ElectricEngine(),
            new DieselEngine()
        };

    private static List<ITransmission> transmissions = new List<ITransmission>
        {
            new ManualTransmission(),
            new AutomaticTransmission()
        };

    private static List<IBody> bodies = new List<IBody>
        {
            new SedanBody(),
            new SUVBody(),
            new HatchbackBody()
        };

    private static List<string> colors = new List<string> { "Red", "Blue", "Black", "White" };
    private static List<string> steeringPositions = new List<string> { "Left", "Right" };

    static void Main()
    {
        Console.Title = "Car Configuration System";
        Console.ForegroundColor = ConsoleColor.Cyan;

        while ( true )
        {
            try
            {
                var car = ConfigureCar();
                Console.Clear();
                car.PrintConfiguration();
                if ( !AskToContinue() ) break;
            }
            catch ( Exception ex )
            {
                ShowError( ex.Message );
            }
        }
    }

    private static Car ConfigureCar()
    {
        Console.Clear();
        Console.WriteLine( "=== Car Configuration Menu ===" );

        IEngine engine = SelectComponent( "\nSelect Engine:", engines );
        ITransmission transmission = SelectComponent( "\nSelect Transmission:", transmissions );
        IBody body = SelectComponent( "\nSelect Body:", bodies );
        string color = SelectOption( "\nSelect Color:", colors );
        string steering = SelectOption( "\nSelect Steering Position:", steeringPositions );

        return new Car( engine, transmission, body, color, steering );
    }

    private static T SelectComponent<T>( string title, List<T> components ) where T : class
    {
        Console.WriteLine( title );
        for ( int i = 0; i < components.Count; i++ )
        {
            Console.WriteLine( $"{i + 1}. {components[ i ]}" );
        }

        int choice = ReadChoice( components.Count );
        return components[ choice - 1 ];
    }

    private static string SelectOption( string title, List<string> options )
    {
        Console.WriteLine( title );
        for ( int i = 0; i < options.Count; i++ )
        {
            Console.WriteLine( $"{i + 1}. {options[ i ]}" );
        }

        int choice = ReadChoice( options.Count );
        return options[ choice - 1 ];
    }

    private static int ReadChoice( int maxOption )
    {
        int choice;
        while ( true )
        {
            Console.Write( $"\nEnter your choice (1-{maxOption}): " );
            if ( int.TryParse( Console.ReadLine(), out choice ) && choice >= 1 && choice <= maxOption )
            {
                return choice;
            }
            ShowError( "Invalid input! Please try again." );
        }
    }

    private static bool AskToContinue()
    {
        Console.Write( "\nConfigure another car? (Y/N): " );
        return Console.ReadLine().Trim().ToUpper() == "Y";
    }

    private static void ShowError( string message )
    {
        var originalColor = Console.ForegroundColor;
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine( $"\nError: {message}" );
        Console.ForegroundColor = originalColor;
        Console.WriteLine( "Press any key to continue..." );
        Console.ReadKey();
    }
}
