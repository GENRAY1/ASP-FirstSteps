using Empty.middlewares.SimpleApi;
using Microsoft.AspNetCore.Builder;

namespace Empty.middlewares
{
    public static class ApiMiddleware
    {
        public static void UseApiMiddleware(this IApplicationBuilder applicationBuilder)
        {
            applicationBuilder.Map("/api", Api);
        }
        static void Api(IApplicationBuilder appBuilder)
        {
            appBuilder.Map("/simple", appBuilder =>
            {
                appBuilder.Map("/users", appBuilder =>
                {
                    UserAPI userAPI = new UserAPI();
                    appBuilder.Run(userAPI.Handler);
                });

            });
        }
    }
}
