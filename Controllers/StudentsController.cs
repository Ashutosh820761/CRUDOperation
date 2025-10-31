using Azure;
using CRUDOperations.DTO;
using CRUDOperations.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace CRUDOperations.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : Controller
    {
        private readonly MyContext _mycontext;

        public StudentsController(MyContext mycontext)
        {
            _mycontext = mycontext;
        }
        //Get Student 
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> GetStudent()
        {
            return await _mycontext.Student.ToListAsync();


        }

        //Get Student By ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudentById(int id)
        {
            var student = await _mycontext.Student.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }
            return student;

        }
        //Create Studuent
        [HttpPost]
        public async Task<ActionResult<Student>> CreateStudent(Student student)
        {
            _mycontext.Student.Add(student);
            await _mycontext.SaveChangesAsync();
            return CreatedAtAction(nameof(GetStudentById), new { id = student.Id }, student);

        }
        //Update Stduent 
        [HttpPut("{id}")]
        
        public async Task<IActionResult> UpdateStudent(int id, Student student)
        {
            if (id != student.Id)
            {
                return BadRequest();
            }

            _mycontext.Entry(student).State = EntityState.Modified;
            await _mycontext.SaveChangesAsync();

            return NoContent();
        }
        //Delete Student By Id
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteStudent(int id)
        {
            var student = await _mycontext.Student.FindAsync(id);
            _mycontext.Student.Remove(student);
            await _mycontext.SaveChangesAsync();

            return NoContent();
        }

        [HttpPatch("UpdateField/{id}")]
        public async Task<IActionResult> UpdateField(int id, [FromQuery] UpdateStudentFieldDTO dto)
        {
            var student = await _mycontext.Student.FindAsync(id);
            if (student == null)
                return NotFound("Student Not Found");

            if (dto.FieldName?.ToLower() == "name")
                student.Name = dto.Value;
            else if (dto.FieldName?.ToLower() == "address")
                student.address = dto.Value;
            else if (dto.FieldName?.ToLower() == "rollno")
                student.RollNo = Convert.ToInt32(dto.Value);
            else if (dto.FieldName?.ToLower() == "graduyear")
                student.GraduYear = Convert.ToInt32(dto.Value);
            else
                return BadRequest("Invalid field name!");

            await _mycontext.SaveChangesAsync();
            return Ok(student);
        }


        /*  public IActionResult Index()
          {
              return View();
          }*/
    }
}
