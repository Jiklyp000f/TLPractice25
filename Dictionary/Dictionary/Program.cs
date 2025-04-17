class Program
{
    static void Main()
    {
        IUserInterface ui = new ConsoleUI();
        var filePath = FileSelector.SelectFile( ui );

        var dictionaryManager = new DictionaryManager( filePath );
        var app = new DictionaryApplication( dictionaryManager, ui );

        try
        {
            app.Run();
        }
        finally
        {
            dictionaryManager.SaveDictionary();
            ui.DisplayMessage( "Нажмите Enter для выхода..." );
            ui.ReadInput( "" );
        }
    }
}
