using Backend.Models;
using Backend.Services;
using FluentValidation;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();



builder.Services.AddScoped<PosiljkeService>();
builder.Services.AddScoped<IValidator<Posiljka>, Backend.Validators.PosiljkaValidator>();


builder.Services.AddControllers();
builder.Services.AddSwaggerGen();



var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapiranje kontrolera
app.MapControllers();

app.UseHttpsRedirection();


app.Run();

