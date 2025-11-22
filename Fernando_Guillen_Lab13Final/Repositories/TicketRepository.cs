using Microsoft.EntityFrameworkCore;
using Fernando_Guillen_Lab13Final.Data;
using Fernando_Guillen_Lab13Final.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Fernando_Guillen_Lab13Final.Repositories
{
    public class TicketRepository : IGenericRepository<Ticket>
    {
        private readonly LINQExampleDbContext _context;

        public TicketRepository(LINQExampleDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Ticket>> GetAllAsync() =>
            await _context.Tickets.Include(t => t.User).ToListAsync();

        public async Task<Ticket> GetByIdAsync(Guid id) =>
            await _context.Tickets
                .Include(t => t.Responses)
                .FirstOrDefaultAsync(t => t.TicketId == id);

        public async Task AddAsync(Ticket entity)
        {
            _context.Tickets.Add(entity);
            await _context.SaveChangesAsync();
        }
    }
}
