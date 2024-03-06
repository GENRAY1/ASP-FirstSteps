using Empty.dbcontext;
using Empty.services.storage.abstractions;
using Empty.services.storage.implementations;
using Microsoft.EntityFrameworkCore;

namespace Empty.services.extentions
{
    public static class StorageServices
    {
        public static void AddStorageServices(this IServiceCollection services, IConfiguration config)
        {
            string? connectionString = config.GetConnectionString("DefaultConnection") ??
                throw new Exception("Строка подключения пустая");

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseSqlServer(connectionString);
            });
            services.AddTransient<IUserRepository, UserRepository>();
        }
    }
}
