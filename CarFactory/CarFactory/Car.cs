public class Car
{
    public IEngine Engine { get; }
    public ITransmission Transmission { get; }
    public IBody Body { get; }
    public string Color { get; }
    public string SteeringPosition { get; }

    public int MaxSpeed => ( int )( Engine.Power * 2 / Body.AerodynamicCoefficient );
    public int Gears => Transmission.Gears;

    public Car( IEngine engine,
              ITransmission transmission,
              IBody body,
              string color,
              string steeringPosition )
    {
        Engine = engine;
        Transmission = transmission;
        Body = body;
        Color = color;
        SteeringPosition = steeringPosition;
    }

    public void PrintConfiguration()
    {
        Console.WriteLine( "Car Configuration:" );
        Console.WriteLine( $"Engine: {Engine.Type} ({Engine.Power} HP)" );
        Console.WriteLine( $"Transmission: {Transmission.Type} ({Gears} gears)" );
        Console.WriteLine( $"Body: {Body.Shape}" );
        Console.WriteLine( $"Color: {Color}" );
        Console.WriteLine( $"Steering: {SteeringPosition}" );
        Console.WriteLine( $"Max Speed: {MaxSpeed} km/h" );
        Console.WriteLine( new string( '=', 30 ) );
    }
}
