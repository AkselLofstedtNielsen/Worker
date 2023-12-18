using Microsoft.EntityFrameworkCore;
using WorkerPlatform.API.Data;
using WorkerPlatform.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(
    options => 
    options.UseSqlServer(builder.Configuration.GetConnectionString("WorkerPlatformConnection"))
);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/employee", () =>
{
    Employee employee = new Employee("Aksel");
    return employee;
});

app.MapPost("/employee", (string name) => {
    return new Employee(name);
});

app.Run();


