using Microsoft.EntityFrameworkCore;

namespace MessageBoardApi.Models

{
  public class MessageBoardApiContext : DbContext
  {
    public MessageBoardApiContext(DbContextOptions<MessageBoardApiContext> options) : base(options)
    {
    }
    public DbSet<Message> Messages { get; set; }
  }
}