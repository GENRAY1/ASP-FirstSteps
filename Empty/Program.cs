namespace Empty;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        app.Run(Foo);
        app.Run();
    }

    static async Task Foo(HttpContext context) { 
        var path = context.Request.Path;
        context.Response.ContentType = "text/html; charset=utf-8";

        switch (path)
        {
            case "/":
                await context.Response.WriteAsync("<h1>Home page</h1>");
                break;
            case "/about":
                await context.Response.WriteAsync("<h1>About page</h1>");
                break;
            default:
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("<h1>Not found</h1> <div>Error 404</div>");
                break;

        }
       
    }
    

}






