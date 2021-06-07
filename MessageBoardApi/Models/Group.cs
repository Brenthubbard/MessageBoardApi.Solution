using System.Collections.Generic;

namespace MessageBoardApi.Models
{
  public class Group
  {
    public int GroupId { get; set; }
    public string Name { get; set; }

    public virtual ICollection<GroupMessage> GroupMessageJoinEntities { get; set; }

    public Group()
    {
      this.GroupMessageJoinEntities = new HashSet<GroupMessage>();
    }
  }
}