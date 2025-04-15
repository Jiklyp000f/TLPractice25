class Program
{
    static void Main()
    {
        var dictionary = new DictionaryManager( "hello", "привет", $"../../../crud.txt" ); //допилить логику с названием файлов (есть задумка выводить список файлов ../../../*.txt и после спрашивать у юзера какой файл он хочет использовать или создать новый...) так же доделать логику выбора файлов при работе программы
        try
        {
            Print( dictionary );
        }
        finally
        {
            dictionary.SaveDictionary();
            Console.WriteLine( "Нажмите Enter для выхода..." );
            Console.ReadLine();
        }
    }

    public static void Print( DictionaryManager dictionary ) //поработать над переиспользованием этого метода, допустим если слова будут приходить не из консоли, а из другого источника, поразмышлять можно ли отвязать логику поиска слова в словаре от логики консоли?
    {
        while ( true )
        {
            Console.WriteLine( "\nСловарь\n" +
                        "1. Перевести слово\n" +
                        "2. Добавить новое слово\n" +
                        "3. Выход" );
            Console.Write( "Выберите действие (1-3): " );
            string choose = Console.ReadLine();
            switch ( choose )
            {
                case "1":
                    HandleTranslation( dictionary );
                    break;
                case "2":
                    HandleAddingWord( dictionary );
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine( "Неверный ввод!!!" );
                    break;
            }
        }
    }

    private static void HandleTranslation( DictionaryManager dictionary )
    {
        Console.Write( "Введите слово для перевода: " );
        string word = Console.ReadLine();

        string translation = dictionary.TranslateWord( word );
        if ( translation != null )
        {
            Console.WriteLine( $"Перевод: {translation}" );
        }
        else
        {
            Console.Write( "Слово не найдено.\nХотите добавить новое слово(Y/N): " );
            if ( Console.ReadLine().ToLower() == "y" )
            {
                HandleAddingWord( dictionary );
            }
        }
    }

    private static void HandleAddingWord( DictionaryManager dictionary )
    {
        Console.Write( "Введите слово: " );
        string word = Console.ReadLine();

        if ( !dictionary.ContainsWord( word ) )
        {
            Console.Write( "Введите перевод: " );
            string translation = Console.ReadLine();

            if ( dictionary.AddWord( word, translation ) )
            {
                Console.WriteLine( "Слово добавлено в словарь!" );
            }
        }
        else
        {
            Console.WriteLine( "Такое слово уже существует!" );
        }
    }
}
