using Core.Interfaces;
using Domain.Entities;
using Google;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.App.Chat.Command
{
    public class SendFriendRequestCommand : IRequest<string>
    {
        public Guid SenderId { get; set; }
        public Guid ReceiverId { get; set; }
    }
    public class FriendRequestHandler : IRequestHandler<SendFriendRequestCommand, string>
    {
        private readonly IAppDbContext _context;

        public FriendRequestHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(SendFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var existingRequest = await _context.Set<FriendRequest>()
                .FirstOrDefaultAsync(fr => fr.SenderId == request.SenderId && fr.ReceiverId == request.ReceiverId);

            if (existingRequest != null)
                return "Friend request already sent.";

            var friendRequest = new FriendRequest
            {
                SenderId = request.SenderId,
                ReceiverId = request.ReceiverId,
                Status = "Pending"
            };

            _context.Set<FriendRequest>().Add(friendRequest);
            await _context.SaveChangesAsync();

            return "Friend request sent.";
        }
    }

}
