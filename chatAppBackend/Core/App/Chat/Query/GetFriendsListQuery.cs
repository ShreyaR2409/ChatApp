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

namespace Core.App.Chat.Query
{
    public class GetFriendsListQuery : IRequest<List<Users>>
    {
        public Guid UserId { get; set; }
    }

    public class GetFriendsListHandler : IRequestHandler<GetFriendsListQuery, List<Users>>
    {
        private readonly IAppDbContext _context;

        public GetFriendsListHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Users>> Handle(GetFriendsListQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<FriendRequest>()
                .Where(fr => (fr.SenderId == request.UserId || fr.ReceiverId == request.UserId) && fr.Status == "Accepted")
                .Select(fr => fr.SenderId == request.UserId ? fr.Receiver : fr.Sender)
                .ToListAsync();
        }
    }
}
