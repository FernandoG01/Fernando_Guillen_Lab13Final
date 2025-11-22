using Microsoft.EntityFrameworkCore;
using Fernando_Guillen_Lab13Final.Data;
using Fernando_Guillen_Lab13Final.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fernando_Guillen_Lab13Final.CQRS.Queries
{
    public class GetTicketsByUserHandler :
        IQueryHandler<GetTicketsByUserQuery, IEnumerable<Ticket>>
    {
        private readonly LINQExampleDbContext _context;

        public GetTicketsByUserHandler(LINQExampleDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Ticket>> Handle(GetTicketsByUserQuery query)
        {
            return await _context.Tickets
                .Where(t => t.UserId == query.UserId)
                .Include(t => t.Responses)
                .ToListAsync();
        }
    }
}