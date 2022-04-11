// See https://aka.ms/new-console-template for more information
namespace UILayer;

public class Program
{
    public static async void Main(String[] args)
    {
        await MenuFactory.GetMenu("main").StartAsync();
    }
}