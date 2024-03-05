using Empty.middlewares;
using Empty.middlewares.SimpleApi;
using Microsoft.AspNetCore.Builder;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection.PortableExecutable;

namespace Empty;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        var app = builder.Build();
        app.UseMiddleware<AuthMiddleware>();
        app.UseApiMiddleware();
        app.UseMyStatic();

        app.Run();
    }
    
}






