public class ConsoleMenu : ISelectionMenu
{
    public T SelectFromCollection<T>( IEnumerable<T> collection, string prompt )
    {
        Console.WriteLine( prompt );
        var items = collection.ToArray();

        for ( int i = 0; i < items.Length; i++ )
        {
            Console.WriteLine( $"{i + 1}. {items[ i ]}" );
        }

        int selectedIndex;
        do
        {
            Console.Write( "Выберите вариант: " );
        } while ( !int.TryParse( Console.ReadLine(), out selectedIndex )
                || selectedIndex < 1
                || selectedIndex > items.Length );

        return items[ selectedIndex - 1 ];
    }
}

