using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using WorkerPlatform.API.Data;
using WorkerPlatform.API.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureHttpJsonOptions(options => {
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
});
builder.Services.AddDbContext<Context>();
// builder.Services.AddDbContext<Context>(
//     options => 
//     options.UseSqlServer(builder.Configuration.GetConnectionString("WorkerPlatformConnection"))
// );

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

app.Run();


