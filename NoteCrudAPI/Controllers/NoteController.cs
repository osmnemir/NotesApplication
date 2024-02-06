using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoteCrudAPI.Data;
using NoteCrudAPI.Models;

namespace NoteCrudAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public NoteController(ApiDbContext context)
        {
            _context = context;

        }



        [HttpGet]
        public async Task<ActionResult<List<Note>>> GetNote()
        {
            return Ok(await _context.Notes.ToListAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<List<Note>>> GetNote(int id)
        {
            var note = _context.Notes.Find(id);
            if (note == null)
            {
                return NotFound();
            }
            return Ok(note);
        }


        [HttpPost]
        public async Task<ActionResult<List<Note>>> Create(Note note)
        {
            _context.Notes.Add(note);
            await _context.SaveChangesAsync();

            return Ok(note);
        }


        [HttpPut("{id}")]
        public async Task<ActionResult<List<Note>>> Update(int id, Note note)
        {
            if (id! == note.Id)

                return BadRequest();
            _context.Entry(note).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<List<Note>>> Delete(int id)
        {
            var note = await _context.Notes.FindAsync(id);
            if (note == null)
            {
                return NotFound();
            }

            _context.Notes.Remove(note);
            await _context.SaveChangesAsync();

            return Ok();
        }

    }
}
