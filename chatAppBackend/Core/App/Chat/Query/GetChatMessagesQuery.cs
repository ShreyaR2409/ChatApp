using Core.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.App.Chat.Query
{
    public class GetChatMessagesQuery : IRequest<List<Domain.Entities.Message>>
    {
        public Guid ChatId { get; set; }
    }
    public class GetChatMessagesQueryHandler : IRequestHandler<GetChatMessagesQuery, List<Domain.Entities.Message>>
    {
        private readonly IAppDbContext _context;

        public GetChatMessagesQueryHandler(IAppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Domain.Entities.Message>> Handle(GetChatMessagesQuery request, CancellationToken cancellationToken)
        {
            return await _context.Set<Domain.Entities.Message>()
           .Where(m => m.ChatId == request.ChatId)
           .OrderBy(m => m.Timestamp)
           .ToListAsync(cancellationToken);
        }
    }
}
