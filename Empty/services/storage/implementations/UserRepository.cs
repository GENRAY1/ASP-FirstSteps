using Empty.dbcontext;
using Empty.models;
using Empty.services.storage.abstractions;
using Microsoft.EntityFrameworkCore;

namespace Empty.services.storage.implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationContext _context;

        public UserRepository(ApplicationContext context) { 
            this._context = context;
        }

        public async Task AddAsync(User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            User? currentUser = await _context.Users.FindAsync(id);
            if (currentUser == null) return;

            _context.Users.Remove(currentUser);
            await _context.SaveChangesAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        public async Task<User?> GetAsync(int id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
