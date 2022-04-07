namespace UILayer;

public class HomeMenu : IMenu
{
    private readonly IBusiness _bl;
    private readonly ILogger _logger;
    private Customer _customer = new Customer();
    private StoreFront _store = new StoreFront();

    public HomeMenu(IBusiness bl, ILogger logger)
    {
        _bl = bl;
        _logger = logger;
    }

    public void Start()
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
                    Order();
                    break;

                case ('H'):
                    OrderHistory();
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

    public void Start(Customer customer)
    {
        _customer = customer;
        Start();
    }

    public void Start(Customer customer, StoreFront store)
    {
        _customer = customer;
        _store = store;
        Start();
    }

    public void Order()
    {
        _store = HelperFunctions.SelectStore(_bl);
        if (_store != null)
        {
            MenuFactory.GetMenu("store").Start(_customer, _store);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Could not select store!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public void OrderHistory()
    {
        List<Order> orders = _bl.GetAllOrders(_customer);
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
    }
}