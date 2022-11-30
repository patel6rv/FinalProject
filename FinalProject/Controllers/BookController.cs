using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly ILogger<BookController> _logger;
    private readonly FinalProjectContext _context;

    public BookController(ILogger<BookController> logger, FinalProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("ById")]
    [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById(int ?id)
    {

        if (id == null || id == 0) return Ok(_context.Books?.ToList().Take(5));
        try
            {
                var book = _context.Books?.Find(id);
                if (book == null)
                {
                    return NotFound("The requested resource was not found");
                }
                return Ok(book);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(typeof(List<Book>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        try
            {
                if (_context.Books == null || !_context.Books.Any())
                    return NotFound("No ToDos found in the database");
                return Ok(_context.Books?.ToList());
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
                var book = _context.Books?.Find(id);
                if (book == null)
                {
                    return NotFound($"Book with id {id} was not found");
                }

                _context.Books?.Remove(book);
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
    public IActionResult Add(Book bookToAdd)
    {
            if (bookToAdd.Id != 0)
            {
                return BadRequest("Id was provided but not needed");
            }
            try
            {
                _context.Books?.Add(bookToAdd);
                var result = _context.SaveChanges();
                if (result >= 1)
                {
                    return Ok($"Book {bookToAdd.Title} added successfully");
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
    public IActionResult Put(Book bookToEdit)
    {
            if (bookToEdit.Id < 1)
            {
                return BadRequest("Please provide a valid id");
            }

            try
            {
                var book = _context.Books?.Find(bookToEdit.Id);
                if (book == null)
                    return NotFound("The Book was not found");

                book.Title = bookToEdit.Title;
                book.Author = bookToEdit.Author;
                book.Description = bookToEdit.Description;
                book.PublishYear = bookToEdit.PublishYear;

                _context.Books?.Update(book);
                var result = _context.SaveChanges();
                if (result >= 1)
                {
                    return Ok("book edited successfully");
                }
                return Problem("Edit was not successful. Please try again");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    
    }
}
