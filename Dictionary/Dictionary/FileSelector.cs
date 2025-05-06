public static class FileSelector
{
    public static string SelectFile( IUserInterface ui )
    {
        var directory = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "../../../" );
        var files = Directory.GetFiles( directory, "*.txt" )
            .Select( Path.GetFileName )
            .ToList();

        ui.DisplayMessage( "Доступные файлы:" );
        for ( int i = 0; i < files.Count; i++ )
        {
            ui.DisplayMessage( $"{i + 1}. {files[ i ]}" );
        }

        while ( true )
        {
            var choice = ui.ReadInput( "Выберите файл (или 'new' для создания): " );

            if ( choice.ToLower() == "new" )
            {
                return Path.Combine( directory, $"dictionary_{DateTime.Now:yyyyMMddHHmmss}.txt" );
            }

            if ( int.TryParse( choice, out int index ) && index > 0 && index <= files.Count )
            {
                return Path.Combine( directory, files[ index - 1 ] );
            }

            ui.DisplayMessage( "Неверный выбор!" );
        }
    }
}