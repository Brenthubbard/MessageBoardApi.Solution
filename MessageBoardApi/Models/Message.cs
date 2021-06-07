using System;

namespace MessageBoardApi.Models
{

  public class Message
  {
    public int MessageId { get; set; }
    public string User { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Content { get; set; }
  }
}
