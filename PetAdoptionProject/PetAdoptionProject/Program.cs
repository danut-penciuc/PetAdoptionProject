using Application.Validators;
using FluentValidation;
using Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using PetAdoptionProject.Configuration;
using PetAdoptionProject.Extensions;
using PetAdoptionProject.Middlewares;
using SharpGrip.FluentValidation.AutoValidation.Mvc.Extensions;

var builder = WebApplication.CreateBuilder(args);
builder.Services
    .AddControllers();

builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssemblyContaining<CreatePetValidator>();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddCustomSwagger();

builder.Services.Configure<DatabaseSettings>(builder.Configuration.GetSection("ConnectionStrings"));

// DbContext
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        x => x.MigrationsAssembly("Infrastructure"));
});

builder.Services.AddCustomServices();

// Logging (structured logging ready)
builder.Services.AddLogging(logging =>
{
    logging.AddConsole();
});


var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//no auth yet
app.UseAuthorization();

app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MigrateDatabase<ApplicationDbContext>();
    await app.SeedDatabaseAsync();
}

app.Run();