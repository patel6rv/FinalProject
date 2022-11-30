using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class CollegeStudentController : ControllerBase
{
    private readonly ILogger<CollegeStudentController> _logger;
    private readonly FinalProjectContext _context;

    public CollegeStudentController(ILogger<CollegeStudentController> logger, FinalProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("ById")]
    [ProducesResponseType(typeof(CollegeStudent), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById(int ?id)
    {
        if (id == null || id == 0) return Ok(_context.CollegeStudents?.ToList().Take(5));
        try
            {
                var student = _context.CollegeStudents?.Find(id);
                if (student == null)
                {
                    return NotFound("The requested resource was not found");
                }
                return Ok(student);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(typeof(List<CollegeStudent>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        try
            {
                if (_context.CollegeStudents == null || !_context.CollegeStudents.Any())
                    return NotFound("No CollegeStudents found in the database");
                return Ok(_context.CollegeStudents?.ToList());
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    }

    [HttpDelete]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        try
            {
                var student = _context.CollegeStudents?.Find(id);
                if (student == null)
                {
                    return NotFound($"CollegeStudent with id {id} was not found");
                }

                _context.CollegeStudents?.Remove(student);
                var result = _context.SaveChanges();
                if (result >= 1)
                {
                    return Ok("Delete operation was successful");
                }
                return Problem("Delete was not successful. Please try again");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Add(CollegeStudent studentToAdd)
    {
            if (studentToAdd.Id != 0)
            {
                return BadRequest("Id was provided but not needed");
            }
            try
            {
                _context.CollegeStudents?.Add(studentToAdd);
                var result = _context.SaveChanges();
                if (result >= 1)
                {
                    return Ok($"CollegeStudent {studentToAdd.FullName} added successfully");
                }
                return Problem("Add was not successful. Please try again");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    }

    [HttpPut]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Put(CollegeStudent studentToEdit)
    {
        if (studentToEdit.Id < 1)
            {
                return BadRequest("Please provide a valid id");
            }

            try
            {
                var student = _context.CollegeStudents?.Find(studentToEdit.Id);
                if (student == null)
                    return NotFound("The CollegeStudent was not found");

                student.FullName = studentToEdit.FullName;
                student.Birthdate = studentToEdit.Birthdate;
                student.CollegeProgram = studentToEdit.CollegeProgram;
                student.YearInProgram = studentToEdit.YearInProgram;

                _context.CollegeStudents?.Update(student);
                var result = _context.SaveChanges();
                if (result >= 1)
                {
                    return Ok("student edited successfully");
                }
                return Problem("Edit was not successful. Please try again");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    }
}
