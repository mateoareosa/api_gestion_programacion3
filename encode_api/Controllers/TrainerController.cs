namespace encode_api.Controllers;

using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TrainerController : ControllerBase
{
    private readonly ILogger<TrainerController> _logger;
    private readonly TrainerDAO _trainerDAO;

    public TrainerController(ILogger<TrainerController> logger, TrainerDAO trainerDAO)
    {
        _logger = logger;
        _trainerDAO = trainerDAO;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Trainer>), StatusCodes.Status200OK)]
    public IActionResult GetAllTrainers()
    {
        List<Trainer> registros = _trainerDAO.GetAllTrainers();

        return Ok(registros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Trainer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTrainerById(long id)
    {
        Trainer? encontrado = _trainerDAO.ReadTrainerById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        return Ok(encontrado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Trainer), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateTrainer(Trainer trainer)
    {
        if (string.IsNullOrWhiteSpace(trainer.Name) || string.IsNullOrWhiteSpace(trainer.Dni))
        {
            return BadRequest("Name y Dni son obligatorios.");
        }

        Trainer creado = _trainerDAO.CreateTrainer(trainer);
        _logger.LogInformation("Se creó Trainer con Id {Id}.", creado.Id);

        return Created($"/Trainer/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Trainer), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateTrainer(long id, Trainer trainer)
    {
        Trainer? encontrado = _trainerDAO.ReadTrainerById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(trainer.Name) || string.IsNullOrWhiteSpace(trainer.Dni))
        {
            return BadRequest("Name y Dni son obligatorios.");
        }

        trainer.Id = id;
        bool actualizado = _trainerDAO.UpdateTrainer(trainer);

        if (!actualizado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se actualizó Trainer con Id {Id}.", id);

        return Ok(trainer);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteTrainer(long id)
    {
        bool eliminado = _trainerDAO.DeleteTrainerById(id);

        if (!eliminado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se eliminó Trainer con Id {Id}.", id);

        return NoContent();
    }
}
