namespace dao_library;

using entity_library;

public class CourseDAO
{
    public Course CreateCourse(Course course)
    {
        long ultimoId = 0;

        foreach (Course registro in MockDatabase.Courses)
        {
            if (registro.Id > ultimoId)
            {
                ultimoId = registro.Id;
            }
        }

        course.Id = ultimoId + 1;
        MockDatabase.Courses.Add(course);

        return course;
    }

    public Course? ReadCourseById(long id)
    {
        foreach (Course registro in MockDatabase.Courses)
        {
            if (registro.Id == id)
            {
                return registro;
            }
        }

        return null;
    }

    public bool UpdateCourse(Course course)
    {
        Course? encontrado = ReadCourseById(course.Id);

        if (encontrado == null)
        {
            return false;
        }

        encontrado.Name = course.Name;

        return true;
    }

    public bool DeleteCourseById(long id)
    {
        Course? encontrado = ReadCourseById(id);

        if (encontrado == null)
        {
            return false;
        }

        return MockDatabase.Courses.Remove(encontrado);
    }

    public List<Course> GetAllCourses()
    {
        return MockDatabase.Courses;
    }
}
