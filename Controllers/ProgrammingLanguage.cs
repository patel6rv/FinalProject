using Microsoft.AspNetCore.Mvc;

namespace FinalProject.Controllers;

[ApiController]
[Route("[controller]")]
public class ProgrammingLanguageController : ControllerBase
{
    private readonly ILogger<ProgrammingLanguageController> _logger;

    public ProgrammingLanguageController(ILogger<ProgrammingLanguageController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("ById")]
    [ProducesResponseType(typeof(ProgrammingLanguage), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetById(int id)
    {
        return Ok();
    }

    [HttpGet]
    [Route("All")]
    [ProducesResponseType(typeof(List<ProgrammingLanguage>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult GetAll()
    {
        return Ok();
    }

    [HttpDelete]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Delete(int id)
    {
        return Ok();
    }

    [HttpPost]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Add(ProgrammingLanguage languageToAdd)
    {
        return Ok();
    }

    [HttpPut]
    [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(string), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
    public IActionResult Put(ProgrammingLanguage languageToEdit)
    {
        return Ok();
    }
}
