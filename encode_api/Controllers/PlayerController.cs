namespace encode_api.Controllers;

using dao_library;
using entity_library;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class PlayerController : ControllerBase
{
    private readonly ILogger<PlayerController> _logger;
    private readonly PlayerDAO _playerDAO;

    public PlayerController(ILogger<PlayerController> logger, PlayerDAO playerDAO)
    {
        _logger = logger;
        _playerDAO = playerDAO;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<Player>), StatusCodes.Status200OK)]
    public IActionResult GetAllPlayers()
    {
        List<Player> registros = _playerDAO.GetAllPlayers();

        return Ok(registros);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult GetPlayerById(long id)
    {
        Player? encontrado = _playerDAO.ReadPlayerById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        return Ok(encontrado);
    }

    [HttpPost]
    [ProducesResponseType(typeof(Player), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public IActionResult CreatePlayer(Player player)
    {
        if (string.IsNullOrWhiteSpace(player.Name) || string.IsNullOrWhiteSpace(player.Dni))
        {
            return BadRequest("Name y Dni son obligatorios.");
        }

        Player creado = _playerDAO.CreatePlayer(player);
        _logger.LogInformation("Se creó Player con Id {Id}.", creado.Id);

        return Created($"/Player/{creado.Id}", creado);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(typeof(Player), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult UpdatePlayer(long id, Player player)
    {
        Player? encontrado = _playerDAO.ReadPlayerById(id);

        if (encontrado == null)
        {
            return NotFound();
        }

        if (string.IsNullOrWhiteSpace(player.Name) || string.IsNullOrWhiteSpace(player.Dni))
        {
            return BadRequest("Name y Dni son obligatorios.");
        }

        player.Id = id;
        bool actualizado = _playerDAO.UpdatePlayer(player);

        if (!actualizado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se actualizó Player con Id {Id}.", id);

        return Ok(player);
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public IActionResult DeletePlayer(long id)
    {
        bool eliminado = _playerDAO.DeletePlayerById(id);

        if (!eliminado)
        {
            return NotFound();
        }

        _logger.LogInformation("Se eliminó Player con Id {Id}.", id);

        return NoContent();
    }
}
