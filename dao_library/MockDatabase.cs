namespace dao_library;

using entity_library;

public static class MockDatabase
{
    public static List<Person> Persons { get; set; } = new List<Person>()
    {
        new Person { Id = 1, Name = "Ana Perez", Age = 22, Dni = "45000001" },
        new Person { Id = 2, Name = "Luis Gomez", Age = 25, Dni = "45000002" },
        new Person { Id = 3, Name = "Sofia Lopez", Age = 20, Dni = "45000003" }
    };

    public static List<Trainer> Trainers { get; set; } = new List<Trainer>()
    {
        new Trainer { Id = 1, Name = "Carlos Diaz", Age = 40, Dni = "30000001" },
        new Trainer { Id = 2, Name = "Laura Ruiz", Age = 35, Dni = "30000002" },
        new Trainer { Id = 3, Name = "Pablo Torres", Age = 38, Dni = "30000003" }
    };

    public static List<Player> Players { get; set; } = new List<Player>()
    {
        new Player { Id = 1, Name = "Juan Castro", Age = 19, Dni = "47000001", Numero = 10 },
        new Player { Id = 2, Name = "Martin Rios", Age = 21, Dni = "47000002", Numero = 7 },
        new Player { Id = 3, Name = "Diego Vega", Age = 20, Dni = "47000003", Numero = 9 }
    };

    public static List<Team> Teams { get; set; } = new List<Team>()
    {
        new Team { Id = 1, Name = "Central", Category = "Primera" },
        new Team { Id = 2, Name = "Union", Category = "Reserva" },
        new Team { Id = 3, Name = "Deportivo Norte", Category = "Juveniles" }
    };

    public static List<Student> Students { get; set; } = new List<Student>()
    {
        new Student { Id = 1, Name = "Lucia Romero", Age = 20, Dni = "46000001", File = "ST001" },
        new Student { Id = 2, Name = "Tomas Molina", Age = 22, Dni = "46000002", File = "ST002" },
        new Student { Id = 3, Name = "Valentina Silva", Age = 21, Dni = "46000003", File = "ST003" }
    };

    public static List<Course> Courses { get; set; } = new List<Course>()
    {
        new Course { Id = 1, Name = "Programacion" },
        new Course { Id = 2, Name = "Base de datos" },
        new Course { Id = 3, Name = "Matematica" }
    };

    public static List<Activity> Activities { get; set; } = new List<Activity>()
    {
        new Activity
        {
            Id = 1,
            Title = "Crear entidades",
            Description = "Programar las clases del sistema.",
            Date = new DateTime(2026, 8, 28)
        },
        new Activity
        {
            Id = 2,
            Title = "Crear los DAO",
            Description = "Agregar los metodos CRUD.",
            Date = new DateTime(2026, 8, 29)
        },
        new Activity
        {
            Id = 3,
            Title = "Practica de repaso",
            Description = null,
            Date = new DateTime(2026, 8, 30)
        }
    };
}
