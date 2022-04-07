namespace UILayer;

public class ManagerMenu : IMenu
{
    private readonly IBusiness _bl;
    private readonly ILogger _logger;
    private Customer _customer = new Customer();
    private StoreFront _store = new StoreFront();

    public ManagerMenu(IBusiness bl, ILogger logger)
    {
        _bl = bl;
        _logger = logger;
    }
    public void Start()
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
                    AddStore();
                    break;

                case ('I'):
                    AddInventory();
                    break;

                case ('E'):
                    AddEmployee();
                    break;

                case ('R'):
                    RemoveEmployee();
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
    public void AddStore()
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

        if (_bl.GetStore(city) == null)
        {
            StoreFront store = new StoreFront
            {
                City = city,
                State = state
            };
            _bl.AddStore(store);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("The city already exists!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }

    public void AddInventory()
    {
        Console.WriteLine("Adding inventory to store!");
        _store = HelperFunctions.SelectStore(_bl);
        if (_store != null)
        {
            MenuFactory.GetMenu("store").Start(_customer, _store);
        }
    }

    public Customer SelectEmployee(bool employee)
    {
        Console.WriteLine("Select a person!");

        List<Customer> customers = _bl.GetAllCustomers(employee);

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

    public void AddEmployee()
    {
        bool employee = false;
        Console.WriteLine("Adding an employee!");
        Customer customer = SelectEmployee(employee);
        if (customer != null)
        {
            _bl.UpdateCustomer(customer);
        }
    }

    public void RemoveEmployee()
    {
        bool employee = true;
        Console.WriteLine("Removing an employee!");
        Customer customer = SelectEmployee(employee);
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
                _bl.UpdateCustomer(customer);
            }
        }
    }
}