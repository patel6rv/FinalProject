using Microsoft.AspNetCore.Mvc;
using FinalProject.Models;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class OlympicAthleteController : ControllerBase
{
    private readonly ILogger<OlympicAthleteController> _logger;
    private readonly FinalProjectContext _context;

    public OlympicAthleteController(ILogger<OlympicAthleteController> logger, FinalProjectContext context)
    {
        _logger = logger;
        _context = context;
    }

    [HttpGet]
    [Route("ById")]
    [ProducesResponseType(typeof(OlympicAthlete), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById(int ?id)
    {
        if (id == null || id == 0) return Ok(_context.OlympicAthletes?.ToList().Take(5));
        try
        {
            var athlete = _context.OlympicAthletes?.Find(id);
            if (athlete == null)
            {
                return NotFound("The requested resource was not found");
            }
            return Ok(athlete);
        }
        catch (Exception e)
        {
            return Problem(e.Message);
        }
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(typeof(List<OlympicAthlete>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        try
        {
            if (_context.OlympicAthletes == null || !_context.OlympicAthletes.Any())
                return NotFound("No OlympicAthletes found in the database");
            return Ok(_context.OlympicAthletes?.ToList());
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
            var athlete = _context.OlympicAthletes?.Find(id);
            if (athlete == null)
            {
                return NotFound($"OlympicAthlete with id {id} was not found");
            }

            _context.OlympicAthletes?.Remove(athlete);
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
    public IActionResult Add(OlympicAthlete athleteToAdd)
    {
        if (athleteToAdd.Id != 0)
        {
            return BadRequest("Id was provided but not needed");
        }
        try
        {
            _context.OlympicAthletes?.Add(athleteToAdd);
            var result = _context.SaveChanges();
            if (result >= 1)
            {
                return Ok($"OlympicAthlete {athleteToAdd.AthleteName} added successfully");
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
    public IActionResult Put(OlympicAthlete athleteToEdit)
    {

        if (athleteToEdit.Id < 1)
        {
            return BadRequest("Please provide a valid id");
        }

        try
        {
            var athlete = _context.OlympicAthletes?.Find(athleteToEdit.Id);
            if (athlete == null)
                return NotFound("The OlympicAthlete was not found");

            athlete.AthleteName = athleteToEdit.AthleteName;
            athlete.Country = athleteToEdit.Country;
            athlete.Sport = athleteToEdit.Sport;
            athlete.MedalsWon = athleteToEdit.MedalsWon;

            _context.OlympicAthletes?.Update(athlete);
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