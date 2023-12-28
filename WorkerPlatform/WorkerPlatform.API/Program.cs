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
 
builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapGet("/employee", (Context context) =>
{
    return context.Employees.Include("WorkFields").ToList();
});

app.MapPost("/employee", (Context context, string name) => {
    Employee employee = new Employee(name); 
    WorkField workField = context.WorkFields.First();
    employee.WorkFields.Add(workField);
    context.Employees.Add(employee);
    context.SaveChanges();
});

app.MapDelete("/employee/{id}", async (Context context, int id) =>
{
    var employee = await context.Employees.FindAsync(id);
    if (employee == null)
    {
        return Results.NotFound();
    }

    context.Employees.Remove(employee);
    await context.SaveChangesAsync();

    return Results.NoContent();
});

app.MapPut("/employee/{id}", async (Context context, int id, Employee updatedEmployee) =>
{
    var employee = await context.Employees.FindAsync(id);
    if (employee == null)
    {
        return Results.NotFound();
    }

    employee.Name = updatedEmployee.Name; 
    
    await context.SaveChangesAsync();

    return Results.Ok(employee);
});

app.MapGet("/workField", (Context context) =>
{
    return context.WorkFields.Include("Employees").ToList();
});

app.MapPost("/workField", (Context context, string name) =>
{
    
        context.WorkFields.Add(new WorkField(name));
        context.SaveChanges();

});

app.MapGet("/manager", (Context context) =>
{
    return context.Managers.Include("WorkFields").ToList();
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
        return Results.NotFound();
    }

    manager.Name = updatedManager.Name;

    await context.SaveChangesAsync();

    return Results.Ok(manager);
});
app.MapDelete("/manager/{id}", async (Context context, int id) =>
{
    var manager = await context.Managers.FindAsync(id);
    if (manager == null)
    {
        return Results.NotFound();
    }

    context.Managers.Remove(manager);
    await context.SaveChangesAsync();


    return Results.NoContent();
});

app.Run();


