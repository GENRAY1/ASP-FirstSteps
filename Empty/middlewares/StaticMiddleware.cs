namespace Empty.middlewares
{
    public static class StaticMiddleware
    {
        public static void UseMyStatic(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Run(async context =>
            {
                var response = context.Response;
                var request = context.Request;

                var path = request.Path;
                var pagePath = $"static/pages/{path}.html";
                response.ContentType = "text/html; charset=utf-8";
                if (path == "/")
                {
                    response.Redirect("/index");
                }

                if (File.Exists(pagePath))
                {
                    await response.SendFileAsync(pagePath);
                }
                else
                {
                    var defaultPath = "static/pages/notfound.html";
                    await response.SendFileAsync(defaultPath);
                }
            });
        }
    }
}
