namespace GestioneSagre.Web;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);
        builder.Configuration.AddJsonFile("ocelot.json", optional: false, reloadOnChange: true);

        Startup startup = new(builder.Configuration);

        startup.ConfigureServices(builder.Services);

        WebApplication app = builder.Build();

        startup.Configure(app);

        app.Run();
    }
}