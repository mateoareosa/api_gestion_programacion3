namespace dao_library;

using entity_library;

public class PersonDAO
{
    public Person CreatePerson(Person person)
    {
        long ultimoId = 0;

        foreach (Person registro in MockDatabase.Persons)
        {
            if (registro.Id > ultimoId)
            {
                ultimoId = registro.Id;
            }
        }

        person.Id = ultimoId + 1;
        MockDatabase.Persons.Add(person);

        return person;
    }

    public Person? ReadPersonById(long id)
    {
        foreach (Person registro in MockDatabase.Persons)
        {
            if (registro.Id == id)
            {
                return registro;
            }
        }

        return null;
    }

    public bool UpdatePerson(Person person)
    {
        Person? encontrado = ReadPersonById(person.Id);

        if (encontrado == null)
        {
            return false;
        }

        encontrado.Name = person.Name;
        encontrado.Age = person.Age;
        encontrado.Dni = person.Dni;

        return true;
    }

    public bool DeletePersonById(long id)
    {
        Person? encontrado = ReadPersonById(id);

        if (encontrado == null)
        {
            return false;
        }

        return MockDatabase.Persons.Remove(encontrado);
    }

    public List<Person> GetAllPersons()
    {
        return MockDatabase.Persons;
    }
}
