using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WorkerPlatform.API.Data;
using WorkerPlatform.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.ConfigureHttpJsonOptions(options => {
//    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
//});
builder.Services.AddDbContext<Context>();
 builder.Services.AddDbContext<Context>(
    options => 
     options.UseSqlServer("Server=localhost;Database=master;Trusted_Connection=True;Trust Server Certificate=Yes")
 );

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/employee", (Context context) =>
{
    context.Employees.Include("Workfields").ToList();
});

app.MapPost("/employee", (Context context, string name) => {
    Employee employee = new Employee(name); 
    WorkField workField = context.WorkFields.First();
    employee.WorkFields.Add(workField);
    context.Employees.Add(employee);
    context.SaveChanges();
});
// Endpoint to delete an Employee by ID
app.MapDelete("/employee/{id}", async (Context context, int id) =>
{
    var employee = await context.Employees.FindAsync(id);
    if (employee == null)
    {
        // Return a 404 Not Found response if the employee does not exist
        return Results.NotFound();
    }

    context.Employees.Remove(employee);
    await context.SaveChangesAsync();

    // Return a 204 No Content response indicating successful deletion
    return Results.NoContent();
});

// Endpoint to update an Employee by ID
app.MapPut("/employee/{id}", async (Context context, int id, Employee updatedEmployee) =>
{
    var employee = await context.Employees.FindAsync(id);
    if (employee == null)
    {
        // Return a 404 Not Found response if the employee does not exist
        return Results.NotFound();
    }

    // Update employee properties with the provided values
    employee.Name = updatedEmployee.Name; 

    await context.SaveChangesAsync();

    // Return the updated employee
    return Results.Ok(employee);
});

app.MapGet("/workField", (Context context) =>
{
    context.WorkFields.Include("Employees").ToList();
});

app.MapPost("/workField", (Context context, string name) =>
{
    
        context.WorkFields.Add(new WorkField(name));
        context.SaveChanges();

});

app.MapGet("/manager", (Context context) =>
{
    context.Managers.Include("Workfields").ToList();
});

app.MapPost("/manager", (Context context, string name) => {
    Manager manager = new Manager(name); 
    WorkField workField = context.WorkFields.First();
    manager.WorkFields.Add(workField);
    context.Managers.Add(manager);
    context.SaveChanges();
});
app.MapPut("/manager/{id}", async (Context context, int id, Manager updatedManager) =>
{
    var manager = await context.Managers.FindAsync(id);
    if (manager == null)
    {
        // Return a 404 Not Found response if the manager does not exist
        return Results.NotFound();
    }

    // Update manager properties with the provided values
    manager.Name = updatedManager.Name;

    await context.SaveChangesAsync();

    // Return the updated manager
    return Results.Ok(manager);
});
app.MapDelete("/manager/{id}", async (Context context, int id) =>
{
    var manager = await context.Managers.FindAsync(id);
    if (manager == null)
    {
        // Return a 404 Not Found response if the manager does not exist
        return Results.NotFound();
    }

    context.Managers.Remove(manager);
    await context.SaveChangesAsync();

    // Return a 204 No Content response indicating successful deletion
    return Results.NoContent();
});

app.Run();


