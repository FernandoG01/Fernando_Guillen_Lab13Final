using Microsoft.EntityFrameworkCore;
using Fernando_Guillen_Lab13Final.Data;
using Fernando_Guillen_Lab13Final.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fernando_Guillen_Lab13Final.CQRS.Queries
{
    public class GetResponsesByTicketHandler :
        IQueryHandler<GetResponsesByTicketQuery, IEnumerable<Response>>
    {
        private readonly LINQExampleDbContext _context;
        public GetResponsesByTicketHandler(LINQExampleDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Response>> Handle(GetResponsesByTicketQuery query)
        {
            return await _context.Responses
                .Where(r => r.TicketId == query.TicketId)
                .Include(r => r.Responder)
                .ToListAsync();
        }
    }
}