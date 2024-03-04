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
            /*
            case "/user":
                var htmlResponse = new System.Text.StringBuilder("<h1>User page</h1><h3>Параметры строки запроса</h3><table>");
                htmlResponse.Append("<tr><td>Параметр</td><td>Значение</td></tr>");
                foreach (var param in context.Request.Query)
                {
                    htmlResponse.Append($"<tr><td>{param.Key}</td><td>{param.Value}</td></tr>");
                }
                htmlResponse.Append("</table>");
                await context.Response.WriteAsync(htmlResponse.ToString());
                break;
            */
            case "/user":
                //https://localhost:7078/user?username=Genray&login=admin&pass=1234
                string? username = context.Request.Query["username"];
                string? login = context.Request.Query["login"];
                string? password = context.Request.Query["pass"];

                await context.Response.WriteAsync($"<h1>Пользователь {username}</h2>" +
                    $"<div>login: {login}</div>" +
                    $"<div>password: {password}</div>"
                    ); 
                break;
            default:
                context.Response.StatusCode = 404;
                await context.Response.WriteAsync("<h1>Not found</h1> <div>Error 404</div>");
                break;
        }
       
    }
    

}






