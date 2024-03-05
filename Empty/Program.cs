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

        app.UseApiMiddleware();
        app.UseMyStatic();

        app.Run();
    }

    static void ApiMiddleware(IApplicationBuilder appBuilder)
    {
        /*
        appBuilder.Map("/users", appBuilder =>
        {
             appBuilder.Run(a);
        });*/
    }

    static async Task StaticMiddleware(HttpContext context) {
        var response = context.Response;
        var request = context.Request;

        var path = request.Path;
        var pagePath = $"static/pages/{path}.html";
        response.ContentType = "text/html; charset=utf-8";
        if(path == "/")
        {
            response.Redirect("/index");
        }

        if (File.Exists(pagePath))
        {
            await response.SendFileAsync(pagePath);
        }
        else {
            var defaultPath = "static/pages/notfound.html";
            await response.SendFileAsync(defaultPath);
        }
       
    }
    

}






