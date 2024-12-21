
using Backend.Models;
using Backend.Repositories;
using Backend.Services;
using FluentValidation;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


Log.Logger = new LoggerConfiguration()
    .WriteTo.Console()
    .CreateLogger();




builder.Services.AddScoped<IPosiljkaRepository, PosiljkaRepository>();
builder.Services.AddScoped<PosiljkeService>();

builder.Services.AddScoped<IValidator<Posiljka>, Backend.Validators.PosiljkaValidator>();

builder.Services.AddSingleton<AuthService>(); 


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


app.MapControllers();

app.UseHttpsRedirection();


app.Run();

