using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using PetCareSystem.Data.EF;
using PetCareSystem.Data.Entites;

namespace PetCareSystem.WebApp.Hubs
{
    public class ChatHub : Hub
{
    private readonly ChatContext _context;

    public ChatHub(ChatContext context)
    {
        _context = context;
    }

    public async Task SendMessage(string username, string messageText)
    {
         var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);

        if (user != null)
        {
            var chatMessage = new Message
            {
                SenderId = user.Id,  // Assign the UserId to SenderId
                Text = messageText,
                Timestamp = DateTime.UtcNow
            };

            _context.Messages.Add(chatMessage);
            await _context.SaveChangesAsync();

            // Broadcast the message to all clients
            await Clients.All.SendAsync("ReceiveMessage", username, messageText);
        }
        else
        {
            // Handle case where user is not found
            // For example, log an error or return a message to the client
            await Clients.Caller.SendAsync("ErrorMessage", "User not found.");
        }
    }
}
}

