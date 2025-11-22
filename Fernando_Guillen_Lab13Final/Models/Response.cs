using System;
using System.Text.Json.Serialization;

namespace Fernando_Guillen_Lab13Final.Models
{
    public class Response
    {
        public Guid ResponseId { get; set; }
        public Guid TicketId { get; set; }
        public Guid ResponderId { get; set; }
        public string Message { get; set; }
        public DateTime CreatedAt { get; set; }

        [JsonIgnore]
        public Ticket Ticket { get; set; }
        [JsonIgnore]
        public User Responder { get; set; }
    }
}