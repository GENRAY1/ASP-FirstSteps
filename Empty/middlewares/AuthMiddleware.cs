namespace Empty.middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;
        private const string acceptToken = "1234";
        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            context.Response.ContentType = "text/html; charset=utf-8";
            var clientToken = context.Request.Query["token"];
            if (String.IsNullOrEmpty(clientToken))
            {
                await context.Response.WriteAsync($"Отсутсвует параметр: токен авторизации (?token={acceptToken})");
                return;
            }

            if (clientToken != acceptToken)
            {
                await context.Response.WriteAsync("Неверный токен авторизации");
            }
            else
            {
                await next.Invoke(context);
            }
            
        }

        
    }
}
