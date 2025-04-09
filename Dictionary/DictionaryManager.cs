
public class DictionaryManager
{
    private Dictionary<string, string> dictionary = new Dictionary<string, string>();
    private static readonly string FileName = Path.Combine( AppDomain.CurrentDomain.BaseDirectory, "../../../dictionary.txt" );

    public DictionaryManager()
    {
        Console.WriteLine( "Конструктор 1 вызван" );
        dictionary = new Dictionary<string, string>();
        LoadDictionaryManager();
        Console.WriteLine( "Конструктор 1 отработал" );
    }
    public DictionaryManager( string initialWord, string initialTranslation ) : this()
    {
        Console.WriteLine( "Конструктор 2 вызван" ); // Логирование вызова

        // Сначала добавляем начальные данные (если переданы)
        if ( !string.IsNullOrEmpty( initialWord ) && !string.IsNullOrEmpty( initialTranslation ) )
        {
            if ( !dictionary.ContainsKey( initialWord.ToLower() ) )
            {
                AddWord( initialWord, initialTranslation );
            }
        }

        Console.WriteLine( "Конструктор 2 отработал" );
    }

    private void LoadDictionaryManager()
    {
        try
        {
            string fullPath = Path.GetFullPath( FileName );
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
                    dictionary.Add( parts[ 0 ].Trim(), parts[ 1 ].Trim() );
                }
            }
            Console.WriteLine( $"Загружено {dictionary.Count} записей." );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка при загрузке словаря: {ex.Message}" );
        }
    }

    public string TranslateWord( string word )
    {
        return dictionary.ContainsKey( word.ToLower() )
            ? dictionary[ word.ToLower() ]
            : null;
    }

    public bool AddWord( string word, string translation )
    {
        if ( !dictionary.ContainsKey( word.ToLower() ) )
        {
            dictionary.Add( word.ToLower(), translation );
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
            foreach ( var pair in dictionary )
            {
                lines.Add( $"{pair.Key}:{pair.Value}" );
            }
            File.WriteAllLines( FileName, lines.ToArray() );
            Console.WriteLine( $"Данные сохранены в: {FileName}" );
            Console.WriteLine( $"Количество записей: {dictionary.Count}" );
        }
        catch ( Exception ex )
        {
            Console.WriteLine( $"Ошибка при сохранении словаря: {ex.Message}" );
        }
    }

    public bool ContainsWord( string word )
    {
        return dictionary.ContainsKey( word.ToLower() );
    }
}