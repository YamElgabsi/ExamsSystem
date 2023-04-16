using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ExamSystem.ClassLibary;
using ExamSystem.ServerSide.Models;

namespace ExamSystem.ServerSide.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentExamsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public StudentExamsController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/StudentExams
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentExam>>> GetStudentExams()
        {
            return await _context.StudentExams.ToListAsync();
        }

        // GET: api/StudentExams/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentExam>> GetStudentExam(string id)
        {
            var studentExam = await _context.StudentExams.FindAsync(id);

            if (studentExam == null)
            {
                return NotFound();
            }

            return studentExam;
        }

        // PUT: api/StudentExams/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentExam(string id, StudentExam studentExam)
        {
            if (id != studentExam.Id)
            {
                return BadRequest();
            }

            _context.Entry(studentExam).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExamExists(id))
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

        // POST: api/StudentExams
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentExam>> PostStudentExam(StudentExam studentExam)
        {
            _context.StudentExams.Add(studentExam);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (StudentExamExists(studentExam.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetStudentExam", new { id = studentExam.Id }, studentExam);
        }

        // DELETE: api/StudentExams/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentExam(string id)
        {
            var studentExam = await _context.StudentExams.FindAsync(id);
            if (studentExam == null)
            {
                return NotFound();
            }

            _context.StudentExams.Remove(studentExam);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExamExists(string id)
        {
            return _context.StudentExams.Any(e => e.Id == id);
        }
    }
}
