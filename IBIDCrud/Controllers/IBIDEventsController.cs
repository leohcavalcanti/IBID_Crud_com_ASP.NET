using IBIDCrud.Entities;
using IBIDCrud.Persistence;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace IBIDCrud.Controllers
{
    [Route("api/ibid=events")]
    [ApiController]
    public class IBIDEventsController : ControllerBase
    {
        private readonly IBIDEventsDbContext _context;

        public IBIDEventsController(IBIDEventsDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var ibidEvent = _context.IBIDEvent.Where(d => !d.IsDeleted).ToList();
            return Ok(ibidEvent);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(Guid id)
        {
            var ibidEvent = _context.IBIDEvent
                .Include(ie => ie.Speakers)
                .SingleOrDefault(d => d.Id == id);
            
            if (ibidEvent == null)
            {
                return NotFound();
            }
            return Ok(ibidEvent);
        }

        [HttpPost]
        public IActionResult Post(IBIDEvent ibidEvent)
        {
            _context.IBIDEvent.Add(ibidEvent);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetById), new {id  = ibidEvent.Id}, ibidEvent);
        }

        [HttpPut("{id}")]
        public IActionResult Update(Guid id, IBIDEvent input)
        {
            var ibidEvent = _context.IBIDEvent.SingleOrDefault(d => d.Id == id);

            if (ibidEvent == null)
            {
                return NotFound();
            }

            ibidEvent.Update(input.Title, input.Description, input.StartDate, input.EndDate);

            _context.IBIDEvent.Update(ibidEvent);
            _context.SaveChanges();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(Guid id)
        {
            var ibidEvent = _context.IBIDEvent.SingleOrDefault(d => d.Id == id);

            if (ibidEvent == null)
            {
                return NotFound();
            }

            ibidEvent.Delete();

            _context.SaveChanges();

            return NoContent();
        }

        [HttpPost("{id}/speakers")]
        public IActionResult PostSpeaker(Guid id, IBIDEventSpeaker speaker)
        {
            speaker.IBIDEventId = id;
            var ibidEvent = _context.IBIDEvent.Any(d => d.Id == id);

            if (!ibidEvent)
            {
                return NotFound();
            }

            _context.IBIDEventSpeakers.Add(speaker);
            _context.SaveChanges();
            return NoContent();
        }

    }
}
