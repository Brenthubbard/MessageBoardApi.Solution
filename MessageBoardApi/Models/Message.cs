using System;
using System.Collections.Generic;

namespace MessageBoardApi.Models
{

  public class Message
  {
    public int MessageId { get; set; }
    public string User { get; set; }
    public DateTime TimeStamp { get; set; }
    public string Content { get; set; }

    public virtual ICollection<GroupMessage> GroupMessageJoinEntities { get; set; }

    public Message()
    {
      this.GroupMessageJoinEntities = new HashSet<GroupMessage>();
    }
  }
}
