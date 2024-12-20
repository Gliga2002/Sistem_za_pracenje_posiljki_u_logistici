using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();



// Ako hoces jednu instancu tokom cele palikacije koristi Singlton ako zelis novu instancu tokom svakog zahteva koristi Scoped. Zato imas problem sa in memory podacima!!!
 // Register repository and service
        builder.Services.AddScoped<IPosiljkaRepository, PosiljkaRepository>();
        builder.Services.AddScoped<PosiljkeService>();

builder.Services.AddScoped<IValidator<Posiljka>, Backend.Validators.PosiljkaValidator>();
// Dodavanje AuthService u DI kontejner
builder.Services.AddSingleton<AuthService>(); // Koristite AddScoped ili AddTransient ako je potrebno




builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin()
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});




var app = builder.Build();

app.UseCors("AllowAll");


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapiranje kontrolera
app.MapControllers();

app.UseHttpsRedirection();


app.Run();

