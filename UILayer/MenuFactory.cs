namespace UILayer;

public static class MenuFactory
{
    public static IMenu GetMenu(string menu)
    {
        menu = menu.ToLower();

        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("../logs/telescopeLogs.txt").CreateLogger();

        switch (menu)
        {
            case "main":
                return new MainMenu(Log.Logger);
            case "home":
                return new HomeMenu(Log.Logger);
            case "manager":
                return new ManagerMenu(Log.Logger);
            case "store":
                return new StoreMenu(Log.Logger);
            default:
                return new MainMenu(Log.Logger);
        }
    }
}