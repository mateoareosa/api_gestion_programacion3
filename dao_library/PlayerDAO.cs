namespace dao_library;

using entity_library;

public class PlayerDAO
{
    public Player CreatePlayer(Player player)
    {
        long ultimoId = 0;

        foreach (Player registro in MockDatabase.Players)
        {
            if (registro.Id > ultimoId)
            {
                ultimoId = registro.Id;
            }
        }

        player.Id = ultimoId + 1;
        MockDatabase.Players.Add(player);

        return player;
    }

    public Player? ReadPlayerById(long id)
    {
        foreach (Player registro in MockDatabase.Players)
        {
            if (registro.Id == id)
            {
                return registro;
            }
        }

        return null;
    }

    public bool UpdatePlayer(Player player)
    {
        Player? encontrado = ReadPlayerById(player.Id);

        if (encontrado == null)
        {
            return false;
        }

        encontrado.Name = player.Name;
        encontrado.Age = player.Age;
        encontrado.Dni = player.Dni;
        encontrado.Numero = player.Numero;

        return true;
    }

    public bool DeletePlayerById(long id)
    {
        Player? encontrado = ReadPlayerById(id);

        if (encontrado == null)
        {
            return false;
        }

        return MockDatabase.Players.Remove(encontrado);
    }

    public List<Player> GetAllPlayers()
    {
        return MockDatabase.Players;
    }
}
