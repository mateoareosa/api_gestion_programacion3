namespace encode_api.Controllers;

using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
    private readonly ILogger<PersonController> _logger;
    private readonly PersonDAO _personDAO;

    public PersonController(ILogger<PersonController> logger, PersonDAO personDAO)
    {
        _logger = logger;
        _personDAO = personDAO;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Person>), StatusCodes.Status200OK)]
    public IActionResult GetAllPersons()
    {
        List<Person> registros = _personDAO.GetAllPersons();

        return Ok(registros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetPersonById(long id)
    {
        Person? encontrado = _personDAO.ReadPersonById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        return Ok(encontrado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Person), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreatePerson(Person person)
    {
        if (string.IsNullOrWhiteSpace(person.Name) || string.IsNullOrWhiteSpace(person.Dni))
        {
            return BadRequest("Name y Dni son obligatorios.");
        }

        Person creado = _personDAO.CreatePerson(person);
        _logger.LogInformation("Se creó Person con Id {Id}.", creado.Id);

        return Created($"/Person/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Person), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdatePerson(long id, Person person)
    {
        Person? encontrado = _personDAO.ReadPersonById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(person.Name) || string.IsNullOrWhiteSpace(person.Dni))
        {
            return BadRequest("Name y Dni son obligatorios.");
        }

        person.Id = id;
        bool actualizado = _personDAO.UpdatePerson(person);

        if (!actualizado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se actualizó Person con Id {Id}.", id);

        return Ok(person);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePerson(long id)
    {
        bool eliminado = _personDAO.DeletePersonById(id);

        if (!eliminado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se eliminó Person con Id {Id}.", id);

        return NoContent();
    }
}
