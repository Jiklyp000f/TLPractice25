public class ConsoleUI : IUserInterface
{
    public string ReadInput( string prompt )
    {
        Console.Write( prompt );
        return Console.ReadLine();
    }

    public void DisplayMessage( string message ) =>
        Console.WriteLine( message );

    public void DisplayMenu() =>
        DisplayMessage( "\nСловарь\n" +
                       "1. Перевести слово\n" +
                       "2. Добавить новое слово\n" +
                       "3. Выход" );
}