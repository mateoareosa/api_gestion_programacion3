namespace encode_api.Controllers;

using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class CourseController : ControllerBase
{
    private readonly ILogger<CourseController> _logger;
    private readonly CourseDAO _courseDAO;

    public CourseController(ILogger<CourseController> logger, CourseDAO courseDAO)
    {
        _logger = logger;
        _courseDAO = courseDAO;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Course>), StatusCodes.Status200OK)]
    public IActionResult GetAllCourses()
    {
        List<Course> registros = _courseDAO.GetAllCourses();

        return Ok(registros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetCourseById(long id)
    {
        Course? encontrado = _courseDAO.ReadCourseById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        return Ok(encontrado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Course), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateCourse(Course course)
    {
        if (string.IsNullOrWhiteSpace(course.Name))
        {
            return BadRequest("Name es obligatorio.");
        }

        Course creado = _courseDAO.CreateCourse(course);
        _logger.LogInformation("Se creó Course con Id {Id}.", creado.Id);

        return Created($"/Course/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Course), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateCourse(long id, Course course)
    {
        Course? encontrado = _courseDAO.ReadCourseById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(course.Name))
        {
            return BadRequest("Name es obligatorio.");
        }

        course.Id = id;
        bool actualizado = _courseDAO.UpdateCourse(course);

        if (!actualizado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se actualizó Course con Id {Id}.", id);

        return Ok(course);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteCourse(long id)
    {
        bool eliminado = _courseDAO.DeleteCourseById(id);

        if (!eliminado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se eliminó Course con Id {Id}.", id);

        return NoContent();
    }
}
