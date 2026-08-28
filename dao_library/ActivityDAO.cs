namespace dao_library;

using entity_library;

public class ActivityDAO
{
    public Activity CreateActivity(Activity activity)
    {
        long ultimoId = 0;

        foreach (Activity registro in MockDatabase.Activities)
        {
            if (registro.Id > ultimoId)
            {
                ultimoId = registro.Id;
            }
        }

        activity.Id = ultimoId + 1;
        MockDatabase.Activities.Add(activity);

        return activity;
    }

    public Activity? ReadActivityById(long id)
    {
        foreach (Activity registro in MockDatabase.Activities)
        {
            if (registro.Id == id)
            {
                return registro;
            }
        }

        return null;
    }

    public bool UpdateActivity(Activity activity)
    {
        Activity? encontrado = ReadActivityById(activity.Id);

        if (encontrado == null)
        {
            return false;
        }

        encontrado.Title = activity.Title;
        encontrado.Description = activity.Description;
        encontrado.Date = activity.Date;
        encontrado.TypeActivity = activity.TypeActivity;

        return true;
    }

    public bool DeleteActivityById(long id)
    {
        Activity? encontrado = ReadActivityById(id);

        if (encontrado == null)
        {
            return false;
        }

        return MockDatabase.Activities.Remove(encontrado);
    }

    public List<Activity> GetAllActivities()
    {
        return MockDatabase.Activities;
    }
}
