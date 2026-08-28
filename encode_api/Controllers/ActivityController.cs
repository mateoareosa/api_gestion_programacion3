namespace encode_api.Controllers;

using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class ActivityController : ControllerBase
{
    private readonly ILogger<ActivityController> _logger;
    private readonly ActivityDAO _activityDAO;

    public ActivityController(ILogger<ActivityController> logger, ActivityDAO activityDAO)
    {
        _logger = logger;
        _activityDAO = activityDAO;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Activity>), StatusCodes.Status200OK)]
    public IActionResult GetAllActivities()
    {
        List<Activity> registros = _activityDAO.GetAllActivities();

        return Ok(registros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Activity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetActivityById(long id)
    {
        Activity? encontrado = _activityDAO.ReadActivityById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        return Ok(encontrado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Activity), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateActivity(Activity activity)
    {
        if (string.IsNullOrWhiteSpace(activity.Title) || activity.Date == DateTime.MinValue)
        {
            return BadRequest("Title y Date son obligatorios.");
        }

        Activity creado = _activityDAO.CreateActivity(activity);
        _logger.LogInformation("Se creó Activity con Id {Id}.", creado.Id);

        return Created($"/Activity/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Activity), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateActivity(long id, Activity activity)
    {
        Activity? encontrado = _activityDAO.ReadActivityById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(activity.Title) || activity.Date == DateTime.MinValue)
        {
            return BadRequest("Title y Date son obligatorios.");
        }

        activity.Id = id;
        bool actualizado = _activityDAO.UpdateActivity(activity);

        if (!actualizado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se actualizó Activity con Id {Id}.", id);

        return Ok(activity);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteActivity(long id)
    {
        bool eliminado = _activityDAO.DeleteActivityById(id);

        if (!eliminado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se eliminó Activity con Id {Id}.", id);

        return NoContent();
    }
}
