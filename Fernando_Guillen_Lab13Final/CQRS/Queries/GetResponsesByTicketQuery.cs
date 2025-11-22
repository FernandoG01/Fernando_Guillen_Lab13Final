using System;

namespace Fernando_Guillen_Lab13Final.CQRS.Queries
{
    public class GetResponsesByTicketQuery
    {
        public Guid TicketId { get; set; }
    }
}