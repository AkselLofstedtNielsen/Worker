namespace WorkerPlatform.API.Models;

public class WorkField
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;

    public Manager? Manager { get; set; }

    public ICollection<Employee>? Employees { get; set; } = new List<Employee> ();

    // public int AreaId { get; set; } //FK

    public WorkField(string name)
    {
        Name = name;
    }

}
