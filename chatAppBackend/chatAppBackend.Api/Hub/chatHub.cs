using Microsoft.AspNetCore.SignalR;
using System.Text.RegularExpressions;

public class ChatHub : Hub
{
    public async Task SendMessage(string chatId, string senderId, string message)
    {
        await Clients.Group(chatId).SendAsync("ReceiveMessage", senderId, message);
    }

    public async Task JoinChat(string chatId)
    {
        await Groups.AddToGroupAsync(Context.ConnectionId, chatId);
    }
}
