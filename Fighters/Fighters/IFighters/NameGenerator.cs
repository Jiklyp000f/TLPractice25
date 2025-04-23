public static class NameGenerator
{
    private static readonly string[] Names =
    {
        "Грозный Воин", "Хитрый Лис", "Стальной Щит",
        "Молниеносный", "Теневой Призрак", "Кровавый Топор"
    };

    public static string Generate()
    {
        var random = new Random();
        return Names[ random.Next( Names.Length ) ];
    }
}

