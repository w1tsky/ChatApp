using ChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;


namespace ChatApp.Database
{

    public class AppDbContext : IdentityDbContext<User>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options){} 
        public DbSet<Chat> Chats { get; set; }
        public DbSet<Message> Messages { get; set; }

    }

}
