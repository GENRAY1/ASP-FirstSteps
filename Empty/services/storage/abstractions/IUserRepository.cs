using Empty.models;

namespace Empty.services.storage.abstractions
{
    public interface IUserRepository
    {
        Task<User?> GetAsync(int id);
        Task<List<User>> GetAllAsync();
        Task UpdateAsync(User user);
        Task DeleteAsync(int id);
        Task AddAsync(User user);
    }
}
