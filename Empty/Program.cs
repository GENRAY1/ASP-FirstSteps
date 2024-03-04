namespace Empty;
class Program
{
    static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();
        app.Run(MyMiddleware);
        app.Run();
    }
    //middleware 
    static async Task MyMiddleware(HttpContext context) {
        var response = context.Response;
        var request = context.Request;

        var path = request.Path;
        var pagePath = $"static/pages/{path}.html";
        response.ContentType = "text/html; charset=utf-8";

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






