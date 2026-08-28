namespace dao_library;

using entity_library;

public class TrainerDAO
{
    public Trainer CreateTrainer(Trainer trainer)
    {
        long ultimoId = 0;

        foreach (Trainer registro in MockDatabase.Trainers)
        {
            if (registro.Id > ultimoId)
            {
                ultimoId = registro.Id;
            }
        }

        trainer.Id = ultimoId + 1;
        MockDatabase.Trainers.Add(trainer);

        return trainer;
    }

    public Trainer? ReadTrainerById(long id)
    {
        foreach (Trainer registro in MockDatabase.Trainers)
        {
            if (registro.Id == id)
            {
                return registro;
            }
        }

        return null;
    }

    public bool UpdateTrainer(Trainer trainer)
    {
        Trainer? encontrado = ReadTrainerById(trainer.Id);

        if (encontrado == null)
        {
            return false;
        }

        encontrado.Name = trainer.Name;
        encontrado.Age = trainer.Age;
        encontrado.Dni = trainer.Dni;

        return true;
    }

    public bool DeleteTrainerById(long id)
    {
        Trainer? encontrado = ReadTrainerById(id);

        if (encontrado == null)
        {
            return false;
        }

        return MockDatabase.Trainers.Remove(encontrado);
    }

    public List<Trainer> GetAllTrainers()
    {
        return MockDatabase.Trainers;
    }
}
