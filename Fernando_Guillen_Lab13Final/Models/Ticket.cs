using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Fernando_Guillen_Lab13Final.Models
{
    public class Ticket
    {
        public Guid TicketId { get; set; }
        public Guid UserId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? ClosedAt { get; set; }

        [JsonIgnore]
        public User User { get; set; }

        public ICollection<Response> Responses { get; set; }
    }
}