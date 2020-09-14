using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ChatApp.Database;
using ChatApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ChatApp.Controllers {
    public class HomeController : Controller {
        private AppDbContext _ctx;

        public HomeController (AppDbContext ctx) => _ctx = ctx;

        public IActionResult Index () => View ();

        [HttpGet ("{id}")]
        public IActionResult Chat (int id) 
        {
            var chat = _ctx.Chats
                .Include (x => x.Messages)
                .FirstOrDefault(x => x.Id == id);
            return View (chat);
        }

        public async Task<IActionResult> CreateRoom (string name) {
            _ctx.Chats.Add (new Chat {
                Name = name,
                    Type = ChatType.Room
            });

            await _ctx.SaveChangesAsync ();
            return RedirectToAction ("Index");
        }
    }

}