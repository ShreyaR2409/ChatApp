using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Chat
    {
        [Key]
        public Guid ChatId { get; set; }

        [ForeignKey(nameof(User1))]
        public Guid User1Id { get; set; }
        public Users User1 { get; set; }

        [ForeignKey(nameof(User2))]
        public Guid User2Id { get; set; }
        public Users User2 { get; set; }

        public List<Message> Messages { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
