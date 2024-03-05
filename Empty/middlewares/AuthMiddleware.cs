namespace Empty.middlewares
{
    public class AuthMiddleware
    {
        private readonly RequestDelegate next;
        public AuthMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

    }
}
