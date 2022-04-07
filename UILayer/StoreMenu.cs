namespace UILayer;

public class StoreMenu : IMenu
{
    private readonly IBusiness _bl;
    private readonly ILogger _logger;
    private Customer _customer = new Customer();
    private StoreFront _store = new StoreFront();

    public StoreMenu(IBusiness bl, ILogger logger)
    {
        _bl = bl;
        _logger = logger;
    }

    public void Start()
    {
        if (_customer.Employee)
        {
            ManagerStoreMenu();
        }
        else
        {
            CustomerStoreMenu();
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

    public void CustomerStoreMenu()
    {
        bool exit = false;

        while (!exit)
        {
            _store.Inventory = _bl.GetAllProducts(_store) ?? new List<Product>();
            Console.WriteLine("Enter a command: (C)art -- (Q)uit");
            string input = InputValidation.ValidString();

            char command = input.Trim().ToUpper()[0];

            switch (command)
            {
                case ('C'):
                    Cart();
                    exit = !exit;
                    break;

                case ('Q'):
                    exit = !exit;
                    _customer.Cart.Clear();
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect command!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }
    }

    public void AddProducttoCart()
    {
        Product product = HelperFunctions.SelectProduct(_bl, _store);

        if (product != null)
        {
            Console.WriteLine("Amount to add:");
            int amount = InputValidation.ValidInteger();
            int maxAmount = _store.Inventory.Find(x => x.ProductName == product.ProductName)!.ProductQuantity;
            if (amount > 0 && amount <= maxAmount)
            {
                product.ProductQuantity = amount;
                _store.Inventory.Find(x => x.ProductName == product.ProductName)!.ProductQuantity -= amount;
                _customer.CartTotal += product.ProductPrice * product.ProductQuantity;
                _customer.Cart.Add(product);
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Not enough or too many!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }

    public void Cart()
    {
        decimal cartTotal = new decimal();

        foreach (Product product in _customer.Cart)
        {
            cartTotal += product.ProductPrice * product.ProductQuantity;
        }

        _customer.CartTotal = cartTotal;

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("=====================================================================");
            Console.WriteLine("=====================================================================");
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (Product product in _customer.Cart)
            {
                Console.WriteLine(product.ToString());
            }
            Console.WriteLine($"Cart total: {_customer.CartTotal}");
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine("=====================================================================");
            Console.WriteLine("=====================================================================");
            Console.WriteLine("Enter a command: (A)dd Products -- (P)urchase Cart -- (Q)uit and Clear Cart");
            switch (InputValidation.ValidString().Trim().ToUpper()[0])
            {
                case ('A'):
                    AddProducttoCart();
                    break;

                case ('P'):
                    if (PurchaseCart())
                    {
                        _customer.Cart.Clear();
                        exit = !exit;
                    }
                    break;

                case ('Q'):
                    _customer.Cart.Clear();
                    exit = !exit;
                    break;

                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Incorrect command!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    break;
            }
        }
    }

    public bool PurchaseCart()
    {
        try
        {
            _bl.AddOrder(_customer.Cart, _store, _customer);
            return true;
        }
        catch (SqlException ex)
        {
            _logger.Error(ex.Message);
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Had trouble with the database! Try again!");
            Console.ForegroundColor = ConsoleColor.Gray;
            return false;
        }
    }

    public void ManagerStoreMenu()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("=====================================================================");
            Console.WriteLine("=====================================================================");
            Console.WriteLine("Currently In Stock!");
            _store.Inventory = _bl.GetAllProducts(_store);
            if (_store.Inventory != null)
            {
                foreach (Product product in _store.Inventory)
                {
                    Console.WriteLine(product.ToString());
                }
            }
            else
            {
                Console.WriteLine("NOTHING!");
            }
            Console.WriteLine("=====================================================================");
            Console.WriteLine("=====================================================================");
            Console.WriteLine("Enter a command: (A)dd Product -- (C)reate Product -- (Q)uit");
            string input = InputValidation.ValidString();

            char command = input.Trim().ToUpper()[0];

            switch (command)
            {
                case ('A'):
                    AddProduct();
                    break;

                case ('C'):
                    CreateProduct();
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

    public void AddProduct()
    {
        Product product = HelperFunctions.SelectProduct(_bl);
        if (product != null)
        {
            Console.WriteLine("Amount to add:");
            int quantity = InputValidation.ValidInteger();

            if (quantity < 1000)
            {
                product.ProductQuantity = quantity;
                try
                {
                    _bl.AddProduct(product, _store);
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Had trouble adding product to inventory!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    ManagerStoreMenu();
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Enter a reasonable amount!");
                Console.ForegroundColor = ConsoleColor.Gray;
            }
        }
    }

    public void CreateProduct()
    {
        Console.WriteLine("Creating a product!");

        Product product = new Product();

        Console.WriteLine("Enter the name:");
        product.ProductName = InputValidation.ValidString().ToLower();

        Console.WriteLine("Enter the price:");
        decimal price = InputValidation.ValidDecimal();

        if (price < 10000)
        {
            product.ProductPrice = price;
            try
            {
                _bl.AddProduct(product);
            }
            catch (SqlException ex)
            {
                _logger.Error(ex.Message);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Had trouble creating product!");
                Console.ForegroundColor = ConsoleColor.Gray;
                ManagerStoreMenu();
            }
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine("Enter a reasonable price!");
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}