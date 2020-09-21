using System;
using System.Threading.Tasks;
using ChatApp.Database;
using ChatApp.Models;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    [Authorize]
    [Route("[controller]")]
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> room;

        public ChatController(IHubContext<ChatHub> chat)
        {
            room = chat;
        }


        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> JoinRoom(string connectionId, string roomName)
        {
            await room.Groups.AddToGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]/{connectionId}/{roomName}")]
        public async Task<IActionResult> LeaveRoom(string connectionId, string roomName)
        {
            await room.Groups.RemoveFromGroupAsync(connectionId, roomName);
            return Ok();
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendMessage(
            int chatId,
            string message,
            string roomName,
            [FromServices] AppDbContext ctx)
        {

            var Message = new Message
            {
                ChatId = chatId,
                Text = message,
                Name = User.Identity.Name,
                Timestamp = DateTime.Now
            };

            ctx.Messages.Add(Message);
            await ctx.SaveChangesAsync();

            await room.Clients.Group(roomName)
                .SendAsync("RecieveMessage", new
                {
                    Text = Message.Text,
                    Name = Message.Name,
                    Timestamp = Message.Timestamp.ToString("dd/MM/yyy hh:mm:ss")
                });

            return Ok();
        }
    }
}