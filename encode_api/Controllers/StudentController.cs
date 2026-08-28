namespace encode_api.Controllers;

using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly StudentDAO _studentDAO;

    public StudentController(ILogger<StudentController> logger, StudentDAO studentDAO)
    {
        _logger = logger;
        _studentDAO = studentDAO;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Student>), StatusCodes.Status200OK)]
    public IActionResult GetAllStudents()
    {
        List<Student> registros = _studentDAO.GetAllStudents();

        return Ok(registros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetStudentById(long id)
    {
        Student? encontrado = _studentDAO.ReadStudentById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        return Ok(encontrado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Student), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateStudent(Student student)
    {
        if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Dni) || string.IsNullOrWhiteSpace(student.File))
        {
            return BadRequest("Name, Dni y File son obligatorios.");
        }

        Student creado = _studentDAO.CreateStudent(student);
        _logger.LogInformation("Se creó Student con Id {Id}.", creado.Id);

        return Created($"/Student/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Student), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateStudent(long id, Student student)
    {
        Student? encontrado = _studentDAO.ReadStudentById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(student.Name) || string.IsNullOrWhiteSpace(student.Dni) || string.IsNullOrWhiteSpace(student.File))
        {
            return BadRequest("Name, Dni y File son obligatorios.");
        }

        student.Id = id;
        bool actualizado = _studentDAO.UpdateStudent(student);

        if (!actualizado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se actualizó Student con Id {Id}.", id);

        return Ok(student);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteStudent(long id)
    {
        bool eliminado = _studentDAO.DeleteStudentById(id);

        if (!eliminado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se eliminó Student con Id {Id}.", id);

        return NoContent();
    }
}
