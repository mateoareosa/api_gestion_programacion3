namespace dao_library;

using entity_library;

public class StudentDAO
{
    public Student CreateStudent(Student student)
    {
        long ultimoId = 0;

        foreach (Student registro in MockDatabase.Students)
        {
            if (registro.Id > ultimoId)
            {
                ultimoId = registro.Id;
            }
        }

        student.Id = ultimoId + 1;
        MockDatabase.Students.Add(student);

        return student;
    }

    public Student? ReadStudentById(long id)
    {
        foreach (Student registro in MockDatabase.Students)
        {
            if (registro.Id == id)
            {
                return registro;
            }
        }

        return null;
    }

    public bool UpdateStudent(Student student)
    {
        Student? encontrado = ReadStudentById(student.Id);

        if (encontrado == null)
        {
            return false;
        }

        encontrado.Name = student.Name;
        encontrado.Age = student.Age;
        encontrado.Dni = student.Dni;
        encontrado.File = student.File;

        return true;
    }

    public bool DeleteStudentById(long id)
    {
        Student? encontrado = ReadStudentById(id);

        if (encontrado == null)
        {
            return false;
        }

        return MockDatabase.Students.Remove(encontrado);
    }

    public List<Student> GetAllStudents()
    {
        return MockDatabase.Students;
    }
}
