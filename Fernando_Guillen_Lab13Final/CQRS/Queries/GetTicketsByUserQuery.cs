using System;

namespace Fernando_Guillen_Lab13Final.CQRS.Queries
{
    public class GetTicketsByUserQuery
    {
        public Guid UserId { get; set; }
    }
}