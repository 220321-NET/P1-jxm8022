namespace UILayer;

public class HomeMenu : IMenu
{
    private readonly HttpServices _http;
    private readonly ILogger _logger;
    private Customer _customer = new Customer();
    private StoreFront _store = new StoreFront();

    public HomeMenu(HttpServices http, ILogger logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task StartAsync()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Enter a command: (O)rder -- Order (H)istory -- (Q)uit");
            string input = InputValidation.ValidString();

            char command = input.Trim().ToUpper()[0];

            switch (command)
            {
                case ('O'):
                    await OrderAsync();
                    break;

                case ('H'):
                    await OrderHistoryAsync();
                    break;

                case ('Q'):
                    exit = true;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect command!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }
    }

    public async Task StartAsync(Customer customer)
    {
        _customer = customer;
        await StartAsync();
    }

    public async Task StartAsync(Customer customer, StoreFront store)
    {
        _customer = customer;
        _store = store;
        await StartAsync();
    }

    public async Task OrderAsync()
    {
        _store = await HelperFunctions.SelectStoreAsync(_http);
        if (_store != null)
        {
            await MenuFactory.GetMenu("store").StartAsync(_customer, _store);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Could not select store!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public async Task OrderHistoryAsync()
    {
        List<Order> orders = await _http.GetAllOrdersAsync(_customer);
        if (orders != null)
        {
            _customer.Orders = orders;
            Console.WriteLine("=====================================================================");
            Console.WriteLine("=====================================================================\n");
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (Order order in _customer.Orders)
            {
                Console.WriteLine(order.ToString());
            }
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("=====================================================================");
            Console.WriteLine("=====================================================================");
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Customer has no orders!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}