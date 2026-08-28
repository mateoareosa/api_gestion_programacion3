namespace encode_api.Controllers;

using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class TeamController : ControllerBase
{
    private readonly ILogger<TeamController> _logger;
    private readonly TeamDAO _teamDAO;

    public TeamController(ILogger<TeamController> logger, TeamDAO teamDAO)
    {
        _logger = logger;
        _teamDAO = teamDAO;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Team>), StatusCodes.Status200OK)]
    public IActionResult GetAllTeams()
    {
        List<Team> registros = _teamDAO.GetAllTeams();

        return Ok(registros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Team), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetTeamById(long id)
    {
        Team? encontrado = _teamDAO.ReadTeamById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        return Ok(encontrado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Team), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreateTeam(Team team)
    {
        if (string.IsNullOrWhiteSpace(team.Name) || string.IsNullOrWhiteSpace(team.Category))
        {
            return BadRequest("Name y Category son obligatorios.");
        }

        Team creado = _teamDAO.CreateTeam(team);
        _logger.LogInformation("Se creó Team con Id {Id}.", creado.Id);

        return Created($"/Team/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Team), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdateTeam(long id, Team team)
    {
        Team? encontrado = _teamDAO.ReadTeamById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(team.Name) || string.IsNullOrWhiteSpace(team.Category))
        {
            return BadRequest("Name y Category son obligatorios.");
        }

        team.Id = id;
        bool actualizado = _teamDAO.UpdateTeam(team);

        if (!actualizado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se actualizó Team con Id {Id}.", id);

        return Ok(team);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeleteTeam(long id)
    {
        bool eliminado = _teamDAO.DeleteTeamById(id);

        if (!eliminado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se eliminó Team con Id {Id}.", id);

        return NoContent();
    }
}
