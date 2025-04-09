class Program
{
    public static void Main()
    {
        var user = new User();
        var Container = new List<IProduct> { new Milk(), new Bread() };
        Console.Write( "Здравствуйте введите пожалуйста свои данные!\nВаше имя: " );
        user.UserName = Console.ReadLine();
        Console.Write( "Адрес доставки: " );
        user.Adress = Console.ReadLine();
        Menu( Container, user );
    }

    static void Menu( List<IProduct> Container, User user )
    {
        while ( true )
        {
            Console.WriteLine( "Какие товары вы хотите добавить?" );
            ListOfProducts( Container );
            string Element = Console.ReadLine();
            uint x;
            switch ( Element.ToLower() )
            {
                case "1":
                case "молоко":
                    x = CountProduct();
                    Container[ 0 ].CountProduct = x;
                    break;
                case "2":
                case "хлеб":
                    x = CountProduct();
                    Container[ 1 ].CountProduct = x;
                    break;
                case "3":
                case "y":
                    var s = ConfirmOrder( Container, user );
                    if ( s.ToLower() == "y" )
                    {
                        ShowSuccessMessage( Container, user );
                        return;
                    }
                    else
                    {
                        break;
                    }
                case "4":
                case "выход":
                    Environment.Exit( 0 );
                    break;
                default:
                    Console.WriteLine( "Некорректное значение попробуйте заного" );
                    break;
            }
        }

    }

    static void ShowSuccessMessage( List<IProduct> container, User user )
    {
        var orderedProducts = container
            .Where( p => p.CountProduct > 0 )
            .Select( p => $"{p.CountProduct} {p.ProductName.ToLower()}" )
            .ToList();

        if ( orderedProducts.Any() )
        {
            string orderDetails = string.Join( ", ", orderedProducts );
            string deliveryDate = DateTime.Now.AddDays( 3 ).ToString( "dd.MM.yyyy" );
            Console.WriteLine( $"{user.UserName}! Ваш заказ {orderDetails} оформлен! Ожидайте доставку по адресу {user.Adress} к {deliveryDate}" );
        }
        else
        {
            Console.WriteLine( "Заказ не оформлен: вы не выбрали продукты." );
        }
    }

    static string ConfirmOrder( List<IProduct> container, User user )
    {
        // Формируем список продуктов с CountProduct > 0
        var orderedProducts = container
            .Where( p => p.CountProduct > 0 )
            .Select( p => $"{p.CountProduct} {p.ProductName.ToLower()}" ) // Пример: "2 молоко"
            .ToList();

        string orderDetails = orderedProducts.Any()
            ? string.Join( ", ", orderedProducts ) // Объединяем через запятую
            : "ничего не заказано";

        // Выводим сообщение
        Console.WriteLine( $"Здравствуйте, {user.UserName}, вы заказали {orderDetails} на адрес {user.Adress}. Все верно? (Y/N)" );
        return Console.ReadLine();
    }

    static uint CountProduct()
    {
        Console.WriteLine( "Какое количество товара вы хотите добавить?" );
        while ( true )
        {
            Console.Write( "Введите количество: " );
            if ( uint.TryParse( Console.ReadLine(), out uint x ) )
                return x;
            Console.WriteLine( "Введите целое положительное число!" );
        }
    }
    static void ListOfProducts( List<IProduct> Container )
    {
        int countProduct = 1;
        foreach ( var product in Container )
        {
            Console.WriteLine( $"{countProduct}. {product.ProductName}" );
            countProduct++;
        }
        Console.WriteLine( $"{countProduct}. Подтвердить заказ?(Y)" );
        countProduct++;
        Console.WriteLine( $"{countProduct}. Выход" );
    }
}
