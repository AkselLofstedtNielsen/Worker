namespace WorkerPlatform.API.Models;

public class WorkField
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Manager? Manager { get; set; }

    public List<Employee>? Employees { get; set; }

    // public int AreaId { get; set; } //FK

    public WorkField(string name)
    {
        Name = name;
    }

}
