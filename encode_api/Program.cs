using dao_library;

var constructor = WebApplication.CreateBuilder(args);

constructor.Services.AddControllers();
constructor.Services.AddScoped<PersonDAO>();
constructor.Services.AddScoped<TrainerDAO>();
constructor.Services.AddScoped<PlayerDAO>();
constructor.Services.AddScoped<TeamDAO>();
constructor.Services.AddScoped<StudentDAO>();
constructor.Services.AddScoped<CourseDAO>();
constructor.Services.AddScoped<ActivityDAO>();
constructor.Services.AddEndpointsApiExplorer();
constructor.Services.AddSwaggerGen();

var aplicacion = constructor.Build();

if (aplicacion.Environment.IsDevelopment())
{
    aplicacion.UseSwagger();
    aplicacion.UseSwaggerUI();

    aplicacion.MapGet("/", () => Results.Redirect("/swagger")).ExcludeFromDescription();
}

aplicacion.UseHttpsRedirection();
aplicacion.UseAuthorization();
aplicacion.MapControllers();

aplicacion.Run();
