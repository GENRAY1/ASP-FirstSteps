using Empty.middlewares.simpleApi;
using Microsoft.AspNetCore.Builder;

namespace Empty.middlewares.extentions
{
    public static class ApiMiddleware
    {
        public static void UseApiMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Map("/api", appBuilder =>
            {
                appBuilder.Map("/simple", appBuilder =>
                {
                    appBuilder.Map("/users", appBuilder =>
                    {
                        appBuilder.UseMiddleware<UserApiMiddleware>();
                    });
                });
            });
        }
    }
}
