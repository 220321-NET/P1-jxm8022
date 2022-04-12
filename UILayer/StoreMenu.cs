namespace UILayer;

public class StoreMenu : IMenu
{
    private readonly HttpServices _http;
    private readonly ILogger _logger;
    private Customer _customer = new Customer();
    private StoreFront _store = new StoreFront();

    public StoreMenu(HttpServices http, ILogger logger)
    {
        _http = http;
        _logger = logger;
    }

    public async Task StartAsync()
    {
        if (_customer.Employee)
        {
            await ManagerStoreMenuAsync();
        }
        else
        {
            await CustomerStoreMenuAsync();
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

    public async Task CustomerStoreMenuAsync()
    {
        bool exit = false;

        while (!exit)
        {
            _store.Inventory = await _http.GetAllProductsAsync(_store) ?? new List<Product>();
            Console.WriteLine("Enter a command: (C)art -- (Q)uit");
            string input = InputValidation.ValidString();

            char command = input.Trim().ToUpper()[0];

            switch (command)
            {
                case ('C'):
                    await CartAsync();
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

    public async Task AddProducttoCartAsync()
    {
        Product product = await HelperFunctions.SelectProductAsync(_http, _store);

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

    public async Task CartAsync()
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
                    await AddProducttoCartAsync();
                    break;

                case ('P'):
                    if (await PurchaseCartAsync())
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

    public async Task<bool> PurchaseCartAsync()
    {
        CustomerOrder customerOrder = new CustomerOrder
        {
            Store = _store,
            Customer = _customer
        };

        try
        {
            await _http.AddOrderAsync(customerOrder);
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

    public async Task ManagerStoreMenuAsync()
    {
        bool exit = false;

        while (!exit)
        {
            Console.WriteLine("=====================================================================");
            Console.WriteLine("=====================================================================");
            Console.WriteLine("Currently In Stock!");
            _store.Inventory = await _http.GetAllProductsAsync(_store);
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
                    await AddProducttoStoreAsync();
                    break;

                case ('C'):
                    await CreateProductAsync();
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

    public async Task AddProducttoStoreAsync()
    {
        StoreOrder storeOrder = new StoreOrder();
        storeOrder.StoreFront = _store;
        storeOrder.Product = await HelperFunctions.SelectProductAsync(_http);
        if (storeOrder.Product != null)
        {
            Console.WriteLine("Amount to add:");
            int quantity = InputValidation.ValidInteger();

            if (quantity < 1000)
            {
                storeOrder.Product.ProductQuantity = quantity;
                try
                {
                    await _http.AddProducttoStoreAsync(storeOrder);
                }
                catch (SqlException ex)
                {
                    _logger.Error(ex.Message);
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Had trouble adding product to inventory!");
                    Console.ForegroundColor = ConsoleColor.Gray;
                    await ManagerStoreMenuAsync();
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

    public async Task CreateProductAsync()
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
                await _http.AddProductAsync(product);
            }
            catch (SqlException ex)
            {
                _logger.Error(ex.Message);
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Had trouble creating product!");
                Console.ForegroundColor = ConsoleColor.Gray;
                await ManagerStoreMenuAsync();
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