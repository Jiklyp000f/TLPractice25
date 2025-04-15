public class Bread : IProduct
{
    public string Name { get; } = "Хлеб";
    public uint StockCount { get; set; } = 0;
}