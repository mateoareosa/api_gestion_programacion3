namespace dao_library;

using entity_library;

public class TeamDAO
{
    public Team CreateTeam(Team team)
    {
        long ultimoId = 0;

        foreach (Team registro in MockDatabase.Teams)
        {
            if (registro.Id > ultimoId)
            {
                ultimoId = registro.Id;
            }
        }

        team.Id = ultimoId + 1;
        MockDatabase.Teams.Add(team);

        return team;
    }

    public Team? ReadTeamById(long id)
    {
        foreach (Team registro in MockDatabase.Teams)
        {
            if (registro.Id == id)
            {
                return registro;
            }
        }

        return null;
    }

    public bool UpdateTeam(Team team)
    {
        Team? encontrado = ReadTeamById(team.Id);

        if (encontrado == null)
        {
            return false;
        }

        encontrado.Name = team.Name;
        encontrado.Category = team.Category;

        return true;
    }

    public bool DeleteTeamById(long id)
    {
        Team? encontrado = ReadTeamById(id);

        if (encontrado == null)
        {
            return false;
        }

        return MockDatabase.Teams.Remove(encontrado);
    }

    public List<Team> GetAllTeams()
    {
        return MockDatabase.Teams;
    }
}
