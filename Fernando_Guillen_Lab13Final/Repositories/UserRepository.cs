using Microsoft.EntityFrameworkCore;
using Fernando_Guillen_Lab13Final.Data;
using Fernando_Guillen_Lab13Final.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fernando_Guillen_Lab13Final.Repositories
{
    public class UserRepository : IGenericRepository<User>
    {
        private readonly LINQExampleDbContext _context;

        public UserRepository(LINQExampleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<User>> GetAllAsync() =>
            await _context.Users.Include(u => u.Tickets).ToListAsync();

        public async Task<User> GetByIdAsync(Guid id) =>
            await _context.Users.FirstOrDefaultAsync(u => u.UserId == id);

        public async Task AddAsync(User entity)
        {
            _context.Users.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}