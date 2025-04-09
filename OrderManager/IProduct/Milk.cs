public class Milk : IProduct
{
    string IProduct.ProductName { get; } = "Молоко";
    uint IProduct.CountProduct { get; set; } = 0;
}
