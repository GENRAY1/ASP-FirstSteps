using Empty.middlewares;
using Empty.middlewares.extentions;
using Empty.services.extentions;

namespace Empty;

class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddStorageServices(builder.Configuration);
        var app = builder.Build();
        app.UseMiddleware<AuthMiddleware>();
        app.UseApiMiddleware();
        app.UseMyStatic();

        app.Run();
    }
    
}






