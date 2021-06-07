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
  public class MessagesController : ControllerBase
  {
    private readonly MessageBoardApiContext _db;

    public MessagesController(MessageBoardApiContext db)
    {
      _db = db;
    }
    //Get api/messages
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Message>>> Get()
    {
      return await _db.Messages.ToListAsync();
    }
    //Post api/messages
    [HttpPost]
    public async Task<ActionResult<Message>> Post(Message message)
    {
      _db.Messages.Add(message);
      await _db.SaveChangesAsync();

      return CreatedAtAction(nameof(GetMessage), new { id = message.MessageId }, message);
    }
    //Get: api/Messages/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Message>> GetMessage(int id)
    {
      var message = await _db.Messages.FindAsync(id);

      if (message == null)
      {
        return NotFound();
      }
      return message;
    }
    // Put: api/Message/5
    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, Message message)
    {
      if (id != message.MessageId)
      {
        return BadRequest();
      }
      _db.Entry(message).State = EntityState.Modified;
      try
      {
        await _db.SaveChangesAsync();
      }
      catch (DbUpdateConcurrencyException)
      {
        if (!MessageExists(id))
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
    private bool MessageExists(int id)
    {
      return _db.Messages.Any(e => e.MessageId == id);
    }
    //Delete: api/Messages
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteMessage(int id)
    {
      var message = await _db.Messages.FindAsync(id);
      if (message == null)
      {
        return NotFound();
      }
      _db.Messages.Remove(message);
      await _db.SaveChangesAsync();

      return NoContent();
    }

    // POST api/messages/{id}/
    [HttpPost("AddGroup/{id}")]
    public async Task<IActionResult> AddGroup(int id, Group group)
    {
      Group selectedGroup = await _db.Groups.FindAsync(group.GroupId);
      Message selectedMessage = await _db.Messages.FindAsync(id);
      if (selectedGroup == null || selectedMessage == null)
      {
        return BadRequest();
      }

      _db.GroupMessage.Add(new GroupMessage() { GroupId = selectedGroup.GroupId, MessageId = selectedMessage.MessageId });
      await _db.SaveChangesAsync();
      return NoContent();
    }
  }
}