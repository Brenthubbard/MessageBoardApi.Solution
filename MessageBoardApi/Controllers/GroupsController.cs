using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MessageBoardApi.Models;
using System.Linq;

namespace MessageBoardApi.Controllers
{
  [Route("api/[controller]")]
  [ApiController]
  public class GroupsController : ControllerBase
  {
    private readonly MessageBoardApiContext _db;

    public GroupsController(MessageBoardApiContext db)
    {
      _db = db;
    }

    // GET api/groups
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Group>>> Get()
    {
      return await _db.Groups.ToListAsync();
    }

    // POST api/groups
    [HttpPost]
    public async Task<ActionResult<Group>> Post(Group group)
    {
      _db.Groups.Add(group);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetGroup), new { id = group.GroupId }, group);
    }

    // GET api/Groups/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<Group>> GetGroup(int id)
    {
      Group group = await _db.Groups.FindAsync(id);

      if (group == null)
      {
        return NotFound();
      }

      return group;
    }

    // PUT api/Groups/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Group group)
    {
      if (id != group.GroupId)
      {
        return BadRequest();
      }

      _db.Entry(group).State = EntityState.Modified;

      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!GroupExists(id))
        {
          return NotFound();
        }
        else
        {
          throw;
        }
      }

      return NoContent();
    }

    private bool GroupExists(int id)
    {
      return _db.Groups.Any(e => e.GroupId == id);
    }

    // DELETE api/Groups/{id}
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteGroup(int id)
    {
      Group group = await _db.Groups.FindAsync(id);
      if (group == null)
      {
        return NotFound();
      }

      _db.Groups.Remove(group);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    // POST api/Groups/AddMessage/{id}
    [HttpPost("AddMessage/{id}")]
    public async Task<IActionResult> AddMessage(int id, Message message)
    {
      Group selectedGroup = await _db.Groups.FindAsync(id);
      Message selectedMessage = await _db.Messages.FindAsync(message.MessageId);
      if (selectedGroup == null || selectedMessage == null)
      {
        return BadRequest();
      }

      _db.GroupMessage.Add(new GroupMessage() 
      { 
        GroupId = selectedGroup.GroupId, 
        MessageId = selectedMessage.MessageId,
        Group = selectedGroup,
        Message = selectedMessage
      });
      await _db.SaveChangesAsync();
      return NoContent();
    }

    // GET api/Groups/GetMessages/{id}
    [HttpGet("GetMessages/{id}")]
    public async Task<ActionResult<IEnumerable<GroupMessage>>> GetMessages(int id)
    {
      List<GroupMessage> joinEntries = await _db.GroupMessage.Where(entry => entry.GroupId == id).ToListAsync();
      return joinEntries;
    }

  }
}