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
    public class AcceptFriendRequestCommand : IRequest<string>
    {
        public Guid RequestId { get; set; }
    }
    public class AcceptFriendRequestHandler : IRequestHandler<AcceptFriendRequestCommand, string>
    {
        private readonly IAppDbContext _context;

        public AcceptFriendRequestHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<string> Handle(AcceptFriendRequestCommand request, CancellationToken cancellationToken)
        {
            var friendRequest = await _context.Set<FriendRequest>()
                .FirstOrDefaultAsync(fr => fr.Id == request.RequestId);

            if (friendRequest == null || friendRequest.Status != "Pending")
                return "Friend request not found.";

            friendRequest.Status = "Accepted";
            await _context.SaveChangesAsync();

            var chat = new Domain.Entities.Chat
            {
                User1Id = friendRequest.SenderId,
                User2Id = friendRequest.ReceiverId
            };

            _context.Set<Domain.Entities.Chat>().Add(chat);
            await _context.SaveChangesAsync();

            return "Friend request accepted, chat created.";
        }
    }

}
