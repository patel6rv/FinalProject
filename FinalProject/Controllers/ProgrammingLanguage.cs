using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgrammingLanguageController : ControllerBase
{
    private readonly ILogger<ProgrammingLanguageController> _logger;
    private readonly FinalProjectContext _context;

    public ProgrammingLanguageController(ILogger<ProgrammingLanguageController> logger, FinalProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("ById")]
    [ProducesResponseType(typeof(ProgrammingLanguage), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById(int ?id)
    {
        if (id == null || id == 0) return Ok(_context.ProgrammingLanguages?.ToList().Take(5));
         try
            {
                var  language= _context.ProgrammingLanguages?.Find(id);
                if (language == null)
                {
                    return NotFound("The requested resource was not found");
                }
                return Ok(language);
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(typeof(List<ProgrammingLanguage>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        try
            {
                if (_context.ProgrammingLanguages == null || !_context.ProgrammingLanguages.Any())
                    return NotFound("No ProgrammingLanguages found in the database");
                return Ok(_context.ProgrammingLanguages?.ToList());
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
                var language = _context.ProgrammingLanguages?.Find(id);
                if (language == null)
                {
                    return NotFound($"ProgrammingLanguage with id {id} was not found");
                }

                _context.ProgrammingLanguages?.Remove(language);
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
    public IActionResult Add(ProgrammingLanguage languageToAdd)
    {
            if (languageToAdd.Id != 0)
            {
                return BadRequest("Id was provided but not needed");
            }
            try
            {
                _context.ProgrammingLanguages?.Add(languageToAdd);
                var result = _context.SaveChanges();
                if (result >= 1)
                {
                    return Ok($"ProgrammingLanguage {languageToAdd.Name} added successfully");
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
    public IActionResult Put(ProgrammingLanguage languageToEdit)
    {
        if (languageToEdit.Id < 1)
            {
                return BadRequest("Please provide a valid id");
            }

            try
            {
                var language = _context.ProgrammingLanguages?.Find(languageToEdit.Id);
                if (language == null)
                    return NotFound("The ProgrammingLanguage was not found");

                language.Name = languageToEdit.Name;
                language.Typed = languageToEdit.Typed;
                language.Execution = languageToEdit.Execution;
                language.Paradigm = languageToEdit.Paradigm;

                _context.ProgrammingLanguages?.Update(language);
                var result = _context.SaveChanges();
                if (result >= 1)
                {
                    return Ok("language edited successfully");
                }
                return Problem("Edit was not successful. Please try again");
            }
            catch (Exception e)
            {
                return Problem(e.Message);
            }
    
    }
}
