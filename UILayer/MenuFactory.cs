namespace UILayer;

public static class MenuFactory
{
    public static IMenu GetMenu(string menu)
    {
        menu = menu.ToLower();

        string connectionString = File.ReadAllText("./connectionString.txt");
        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("../logs/telescopeLogs.txt").CreateLogger();

        IRepository repo = new DBRepository(connectionString);
        IBusiness bl = new Business(repo);

        switch (menu)
        {
            case "main":
                return new MainMenu(bl, Log.Logger);
            case "home":
                return new HomeMenu(bl, Log.Logger);
            case "manager":
                return new ManagerMenu(bl, Log.Logger);
            case "store":
                return new StoreMenu(bl, Log.Logger);
            default:
                return new MainMenu(bl, Log.Logger);
        }
    }
}