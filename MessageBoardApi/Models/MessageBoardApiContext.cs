using Microsoft.EntityFrameworkCore;

namespace MessageBoardApi.Models

{
  public class MessageBoardApiContext : DbContext
  {
    public MessageBoardApiContext(DbContextOptions<MessageBoardApiContext> options) : base(options)
    {
    }
    public DbSet<Message> Messages { get; set; }
    public DbSet<Group> Groups { get; set; }
    public DbSet<GroupMessage> GroupMessage { get; set; }
  }
}