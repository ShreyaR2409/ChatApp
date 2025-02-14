using Core.Interfaces;
using Domain.Entities;
using Google;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.App.Chat.Command
{
    public class SendMessageCommand : IRequest<string>
    {
        public Guid ChatId { get; set; }
        public Guid SenderId { get; set; }
        public string Content { get; set; }
    }
    public class SendMessageHandler : IRequestHandler<SendMessageCommand, string>
    {
        private readonly IAppDbContext _context;

        public SendMessageHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(SendMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message
            {
                ChatId = request.ChatId,
                SenderId = request.SenderId,
                Content = request.Content
            };

            _context.Set<Message>().Add(message);
            await _context.SaveChangesAsync();

            return "Message sent.";
        }
    }

}
