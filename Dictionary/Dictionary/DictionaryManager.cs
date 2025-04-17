
public class DictionaryManager
{
    private readonly Dictionary<string, string> _dictionary = new();
    private readonly string _filePath;
    public event Action<string> Notification;

    public DictionaryManager( string filePath )
    {
        _filePath = filePath;
        LoadDictionary();
    }

    public string TranslateWord( string word ) =>
        _dictionary.TryGetValue( word.ToLower(), out var translation )
            ? translation
            : null;

    public bool AddWord( string word, string translation )
    {
        var lowerWord = word.ToLower();
        if ( _dictionary.TryAdd( lowerWord, translation ) )
        {
            SaveDictionary();
            return true;
        }
        return false;
    }

    public bool ContainsWord( string word ) =>
        _dictionary.ContainsKey( word.ToLower() );

    private void LoadDictionary()
    {
        try
        {
            if ( !File.Exists( _filePath ) )
            {
                File.Create( _filePath ).Close();
                Notification?.Invoke( "Создан новый файл словаря" );
                return;
            }

            foreach ( var line in File.ReadAllLines( _filePath )
                .Where( l => !string.IsNullOrWhiteSpace( l ) ) )
            {
                var parts = line.Split( new[] { ':' }, 2 );
                if ( parts.Length == 2 )
                {
                    _dictionary[ parts[ 0 ].Trim() ] = parts[ 1 ].Trim();
                }
            }
            Notification?.Invoke( $"Загружено {_dictionary.Count} записей" );
        }
        catch ( Exception ex )
        {
            Notification?.Invoke( $"Ошибка загрузки: {ex.Message}" );
        }
    }

    public void SaveDictionary()
    {
        try
        {
            var lines = _dictionary.Select( kv => $"{kv.Key}:{kv.Value}" );
            File.WriteAllLines( _filePath, lines );
            Notification?.Invoke( $"Словарь сохранен ({_dictionary.Count} записей)" );
        }
        catch ( Exception ex )
        {
            Notification?.Invoke( $"Ошибка сохранения: {ex.Message}" );
        }
    }
}
