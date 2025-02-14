using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message
    {
        [Key]
        public Guid MessageId { get; set; }

        [ForeignKey("Chat")]
        public Guid ChatId { get; set; }
        public Chat Chat { get; set; }

        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }
        public Users Sender { get; set; }

        public string Content { get; set; }
        public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    }
}
