
public class DictionaryManager
{
    private Dictionary<string, string> _dictionary = new Dictionary<string, string>();
    private string _fileName;

    public DictionaryManager()
        : this( Path.Combine( AppDomain.CurrentDomain.BaseDirectory, $"../../../dictionary.txt" ) )
    {
    }
    public DictionaryManager( string fileName )
    {
        _fileName = fileName;
        _dictionary = new Dictionary<string, string>();
        LoadDictionaryManager();
    }

    public DictionaryManager( string initialWord, string initialTranslation, string fileName ) : this( fileName )
    {

        // Сначала добавляем начальные данные (если переданы)
        if ( !string.IsNullOrEmpty( initialWord ) && !string.IsNullOrEmpty( initialTranslation ) )
        {
            if ( !_dictionary.ContainsKey( initialWord.ToLower() ) )
            {
                AddWord( initialWord, initialTranslation );
            }
        }
    }

    public string TranslateWord( string word )
    {
        return _dictionary.ContainsKey( word.ToLower() )
            ? _dictionary[ word.ToLower() ]
            : null;
    }

    public bool AddWord( string word, string translation )
    {
        if ( !_dictionary.ContainsKey( word.ToLower() ) )
        {
            _dictionary.Add( word.ToLower(), translation );
            SaveDictionary();
            return true;
        }
        return false;
    }

    public void SaveDictionary()
    {
        try
        {
            List<string> lines = new List<string>();
            foreach ( var pair in _dictionary )
            {
                lines.Add( $"{pair.Key}:{pair.Value}" );
            }
            File.WriteAllLines( _fileName, lines.ToArray() );
            Console.WriteLine( $"Данные сохранены в: {_fileName}" );
            Console.WriteLine( $"Количество записей: {_dictionary.Count}" );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка при сохранении словаря: {ex.Message}" );
        }
    }

    public bool ContainsWord( string word )
    {
        return _dictionary.ContainsKey( word.ToLower() );
    }
    private void LoadDictionaryManager()
    {
        try
        {
            string fullPath = Path.GetFullPath( _fileName );
            Console.WriteLine( $"Загрузка из: {fullPath}" );
            if ( !File.Exists( fullPath ) )
            {
                File.Create( fullPath ).Close();
                Console.WriteLine( $"Файл создан" );
                return;
            }
            string[] lines = File.ReadAllLines( fullPath );
            foreach ( var line in lines )
            {
                var trimmedLine = line.Trim();
                if ( string.IsNullOrEmpty( trimmedLine ) )
                    continue;
                string[] parts = line.Split( ':' );
                if ( parts.Length == 2 )
                {
                    _dictionary.Add( parts[ 0 ].Trim(), parts[ 1 ].Trim() );
                }
            }
            Console.WriteLine( $"Загружено {_dictionary.Count} записей." );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка при загрузке словаря: {ex.Message}" );
        }
    }
}