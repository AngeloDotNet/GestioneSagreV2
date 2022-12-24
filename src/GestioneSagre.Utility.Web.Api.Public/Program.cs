namespace GestioneSagre.Utility.Web.Api.Public;

public class Program
{
    public static void Main(string[] args)
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

        Startup startup = new(builder.Configuration);

        startup.ConfigureServices(builder.Services);

        WebApplication app = builder.Build();

        startup.Configure(app);

        app.Run();
    }
}