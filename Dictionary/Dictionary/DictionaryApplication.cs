public class DictionaryApplication
{
    private readonly DictionaryManager _manager;
    private readonly IUserInterface _ui;

    public DictionaryApplication( DictionaryManager manager, IUserInterface ui )
    {
        _manager = manager;
        _ui = ui;
        _manager.Notification += _ui.DisplayMessage;
    }

    public void Run()
    {
        while ( true )
        {
            _ui.DisplayMenu();
            var choice = _ui.ReadInput( "Выберите действие (1-3): " );

            switch ( choice )
            {
                case "1":
                    HandleTranslation();
                    break;
                case "2":
                    HandleAddingWord();
                    break;
                case "3":
                    return;
                default:
                    _ui.DisplayMessage( "Неверный ввод!!!" );
                    break;
            }
        }
    }

    private void HandleTranslation()
    {
        var word = _ui.ReadInput( "Введите слово для перевода: " );
        var translation = _manager.TranslateWord( word );

        if ( translation != null )
        {
            _ui.DisplayMessage( $"Перевод: {translation}" );
        }
        else
        {
            if ( _ui.ReadInput( "Слово не найдено. Хотите добавить? (Y/N): " ).ToLower() == "y" )
            {
                HandleAddingWord();
            }
        }
    }

    private void HandleAddingWord()
    {
        var word = _ui.ReadInput( "Введите слово: " );

        if ( !_manager.ContainsWord( word ) )
        {
            var translation = _ui.ReadInput( "Введите перевод: " );
            _manager.AddWord( word, translation );
            _ui.DisplayMessage( "Слово добавлено!" );
        }
        else
        {
            _ui.DisplayMessage( "Слово уже существует!" );
        }
    }
}
