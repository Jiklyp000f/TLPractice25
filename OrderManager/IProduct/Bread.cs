public class Bread : IProduct
{
    string IProduct.ProductName { get; } = "Хлеб";
    uint IProduct.CountProduct { get; set; } = 0;
}