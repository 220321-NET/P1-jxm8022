namespace UILayer;

public static class MenuFactory
{
    public static IMenu GetMenu(string menu)
    {
        menu = menu.ToLower();

        Log.Logger = new LoggerConfiguration().MinimumLevel.Debug().WriteTo.File("../logs/telescopeLogs.txt").CreateLogger();

        HttpServices http = new HttpServices(Log.Logger);

        switch (menu)
        {
            case "main":
                return new MainMenu(http, Log.Logger);
            case "home":
                return new HomeMenu(http, Log.Logger);
            case "manager":
                return new ManagerMenu(http, Log.Logger);
            case "store":
                return new StoreMenu(http, Log.Logger);
            default:
                return new MainMenu(http, Log.Logger);
        }
    }
}