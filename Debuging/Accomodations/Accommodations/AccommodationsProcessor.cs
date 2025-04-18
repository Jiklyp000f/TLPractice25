using System.Globalization;
using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor //основной класс выполнения программы
{
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new(); //?? проблема возможно из-за того что int на первом месте, а интерфейс на втором...
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine("Booking Command Line Interface");
        Console.WriteLine("Commands:");
        Console.WriteLine("'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room");
        Console.WriteLine("'cancel <BookingId>' - to cancel a booking");
        Console.WriteLine("'undo' - to undo the last command");
        Console.WriteLine("'find <BookingId>' - to find a booking by ID");
        Console.WriteLine("'search <StartDate> <EndDate> <CategoryName>' - to search bookings");
        Console.WriteLine("'exit' - to exit the application");

        string input;
        while ((input = Console.ReadLine()) != "exit")
        {
            try
            {
                ProcessCommand(input);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
        }
    }

    private static void ProcessCommand(string input)
    {
        string[] parts = input.Split(' ');
        string commandName = parts[0];

        switch (commandName)
        {
            //Переделал весь кейс добавив различные проверки, а конкретно проверка каждого атрибута массива на правильность ввода данных
            case "book":
                if (parts.Length != 6)
                {
                    Console.WriteLine("Invalid number of arguments for booking. Required format: book [userId] [category] [startDate] [endDate] [currency]");
                    return;
                }

                // Валидация UserId
                if (!int.TryParse(parts[1], out int userId))
                {
                    Console.WriteLine($"Invalid user ID: {parts[1]}");
                    return;
                }

                // Валидация категории
                if (string.IsNullOrWhiteSpace(parts[2]))
                {
                    Console.WriteLine("Category cannot be empty");
                    return;
                }

                // Валидация дат
                var dateFormat = "M/d/yyyy";
                var culture = CultureInfo.InvariantCulture;

                if (!DateTime.TryParseExact(parts[3], dateFormat, culture, DateTimeStyles.None, out DateTime startDate))
                {
                    Console.WriteLine($"Invalid start date: {parts[3]}. Use format: {dateFormat} (e.g. 3/15/2024)");
                    return;
                }

                if (!DateTime.TryParseExact(parts[4], dateFormat, culture, DateTimeStyles.None, out DateTime endDate))
                {
                    Console.WriteLine($"Invalid end date: {parts[4]}. Use format: {dateFormat} (e.g. 3/15/2024)");
                    return;
                }

                //Начальная дата не может быть больше конечной даты
                if (startDate >= endDate)
                {
                    Console.WriteLine("Start date must be earlier than end date");
                    return;
                }

                // Валидация валюты
                if (!Enum.TryParse(parts[5], true, out CurrencyDto currency))
                {
                    var validCurrencies = string.Join(", ", Enum.GetNames(typeof(CurrencyDto)));
                    Console.WriteLine($"Invalid currency: {parts[5]}. Valid values: {validCurrencies}");
                    return;
                }

                try
                {
                    var bookingDto = new BookingDto
                    {
                        UserId = userId,
                        Category = parts[2],
                        StartDate = startDate,
                        EndDate = endDate,
                        Currency = currency
                    };

                    var bookCommand = new BookCommand(_bookingService, bookingDto);
                    bookCommand.Execute();
                    _executedCommands.Add(++s_commandIndex, bookCommand);
                    Console.WriteLine("Booking command executed successfully.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error creating booking: {ex.Message}");
                }
                break;

            case "undo":
                _executedCommands[s_commandIndex].Undo(); //при вводе undo срабатывает ошибка, если не вытащил ни одну бронь
                _executedCommands.Remove(s_commandIndex);
                s_commandIndex--;
                Console.WriteLine("Last command undone.");

                break;
            case "find":
                if (parts.Length != 2)
                {
                    Console.WriteLine("Invalid arguments for 'find'. Expected format: 'find <BookingId>'");
                    return;
                }
                Guid id = Guid.Parse(parts[1]); //вот тут баг при вводе find 1, если не вытащил ни одну бронь
                FindBookingByIdCommand findCommand = new(_bookingService, id);
                findCommand.Execute();
                break;

            case "search":
                if (parts.Length != 4)
                {
                    Console.WriteLine("Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'");
                    return;
                }
                DateTime startedDate = DateTime.Parse(parts[1]);
                DateTime endedDate = DateTime.Parse(parts[2]);
                string categoryName = parts[3];
                SearchBookingsCommand searchCommand = new(_bookingService, startedDate, endedDate, categoryName);
                searchCommand.Execute();
                break;

            default:
                Console.WriteLine("Unknown command.");
                break;
        }
    }
}
