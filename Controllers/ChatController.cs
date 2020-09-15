using System.Threading.Tasks;
using ChatApp.Hubs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Controllers
{
    public class ChatController : Controller
    {
        private IHubContext<ChatHub> _chat;

        public ChatController(
            IHubContext<ChatHub> chat)
        {
            _chat = chat;
        }


        // public async Task<IActionResult> JoinChat(int chatId){
        //     await _chat.Groups.AddToGroupAsync();
        //     return Ok();
        // }
    }
}