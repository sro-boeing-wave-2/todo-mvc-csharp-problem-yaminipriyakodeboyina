using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Todolist.Models;
using finaltodo.Models;


namespace finaltodo.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodoesController : ControllerBase
    {
        private readonly finaltodoContext _context;

        public TodoesController(finaltodoContext context)
        {
            _context = context;
        }

       

        [HttpGet]
        public async Task<IActionResult> GetNotes([FromQuery] string title, [FromQuery] string label, [FromQuery] bool? pinned)
        {
            var result = await _context.Todo.Include(n => n.checklist).Include(n => n.label)
                .Where(x => ((title == null || x.heading == title) && (label == null || x.label.Exists(y => y.labelname == label)) && (pinned == null || x.pinned == pinned))).ToListAsync();
            if(result.Count==0)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        [HttpDelete("delete")]
        public async Task<IActionResult> DeleteNotes([FromQuery] string title, [FromQuery] string label, [FromQuery] bool? pinned)
        {
            var result = await _context.Todo.Include(n => n.checklist).Include(n => n.label)
                .Where(x => ((title == null || x.heading == title) && (label == null || x.label.Any(y => y.labelname == label)) && (pinned == null || x.pinned == pinned))).ToListAsync();
            foreach (var note in result)
            {
                _context.Todo.Remove(note);
            }
            await _context.SaveChangesAsync();
            //  _context.Todo.Remove(result.ToList());


            return Ok();
        }






        //// PUT: api/Todoes/5
        //[HttpPut("edit/{id}")]
        //public async Task<IActionResult> PutTodo([FromRoute] int id, [FromBody] Todo todo)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != todo.id)
        //    {
        //        return BadRequest();
        //    }

        //    _context.Todo.Update(todo);
        //    await _context.SaveChangesAsync();

        //    try
        //    {


        //        await _context.SaveChangesAsync();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!TodoExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return Ok(todo);
        //}

           // PUT: api/Notes/5
       [HttpPut("{id}")]
        public async Task<IActionResult> PutNote([FromRoute] int id, [FromBody] Todo note)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != note.id)
            {
                return BadRequest();
            }

            _context.Todo.Update(note);
            //await _context.SaveChangesAsync();

            try
            {


                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TodoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(note);
        }














        // POST: api/Todoes
        [HttpPost]
        public async Task<IActionResult> PostTodo([FromBody] Todo todo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Todo.Add(todo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTodo", new { id = todo.id }, todo);
        }

        // DELETE: api/Todoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTodo([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var todo = await _context.Todo.Include(n => n.checklist).Include(n => n.label).SingleOrDefaultAsync(c => c.id == id);
            if (todo == null)
            {
                return NotFound();
            }

            _context.Todo.Remove(todo);
            await _context.SaveChangesAsync();

            return Ok(todo);
        }

        private bool TodoExists(int id)
        {
            return _context.Todo.Include(n => n.checklist).Include(n => n.label).Any(e => e.id == id);
        }
    }
}