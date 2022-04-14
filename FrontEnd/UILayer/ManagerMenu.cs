namespace UILayer;

public class ManagerMenu : IMenu
{
    private readonly HttpServices _http;
    private readonly ILogger _logger;
    private Customer _customer = new Customer();
    private StoreFront _store = new StoreFront();

    public ManagerMenu(HttpServices http, ILogger logger)
    {
        _http = http;
        _logger = logger;
    }
    public async Task StartAsync()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("Enter a command: (A)dd Store -- Add (I)nventory -- Add (E)mployee -- (R)emove Employee -- (Q)uit");
            string input = InputValidation.ValidString();

            char command = input.Trim().ToUpper()[0];

            switch (command)
            {
                case ('A'):
                    await AddStoreAsync();
                    break;

                case ('I'):
                    await AddInventoryAsync();
                    break;

                case ('E'):
                    await AddEmployeeAsync();
                    break;

                case ('R'):
                    await RemoveEmployeeAsync();
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
    public async Task AddStoreAsync()
    {
        Console.WriteLine("Adding new store!");
        Console.WriteLine("Enter the city:");
        string city = InputValidation.ValidString().ToLower();
    EnterState:
        Console.WriteLine("Enter the state(XX):");
        string state = InputValidation.ValidString().ToUpper();

        if (state.Length > 2)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("State is more than two characters long!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto EnterState;
        }

        if (await _http.GetStoreAsync(city) == null)
        {
            StoreFront store = new StoreFront
            {
                City = city,
                State = state
            };
            await _http.AddStoreAsync(store);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The city already exists!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public async Task AddInventoryAsync()
    {
        Console.WriteLine("Adding inventory to store!");
        _store = await HelperFunctions.SelectStoreAsync(_http);
        if (_store != null)
        {
            await MenuFactory.GetMenu("store").StartAsync(_customer, _store);
        }
    }

    public async Task<Customer> SelectEmployeeAsync(bool employee)
    {
        Console.WriteLine("Select a person!");

        List<Customer> customers = await _http.GetAllCustomersAsync(employee);

        if (customers == null || customers.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("There are no customers!");
            Console.ForegroundColor = ConsoleColor.Gray;
            return null!;
        }

    SelectEmployee:
        for (int i = 0; i < customers.Count; i++)
        {
            Console.WriteLine($"[{i}] {customers[i].UserName}");
        }

        int index;

        if (Int32.TryParse(Console.ReadLine(), out index) && (index >= 0 && index < customers.Count))
        {
            return customers[index];
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a valid index!");
            Console.ForegroundColor = ConsoleColor.Gray;
            goto SelectEmployee;
        }
    }

    public async Task AddEmployeeAsync()
    {
        bool employee = false;
        Console.WriteLine("Adding an employee!");
        Customer customer = await SelectEmployeeAsync(employee);
        if (customer != null)
        {
            await _http.UpdateCustomerAsync(customer);
        }
    }

    public async Task RemoveEmployeeAsync()
    {
        bool employee = true;
        Console.WriteLine("Removing an employee!");
        Customer customer = await SelectEmployeeAsync(employee);
        if (customer != null)
        {
            if (customer.UserName == _customer.UserName)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Cannot remove yourself!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
            else
            {
                await _http.UpdateCustomerAsync(customer);
            }
        }
    }
}