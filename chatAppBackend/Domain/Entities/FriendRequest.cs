using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class FriendRequest
    {
        [Key]
        public Guid Id { get; set; }
        [ForeignKey("Sender")]
        public Guid SenderId { get; set; }
        public Users Sender { get; set; }
        [ForeignKey("Receiver")]
        public Guid ReceiverId { get; set; }
        public Users Receiver { get; set; }
        public string Status { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
