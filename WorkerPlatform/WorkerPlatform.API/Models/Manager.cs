namespace WorkerPlatform.API.Models;
public class Manager
{
    public int Id { get; set; } //PK
    public string Name { get; set;} = string.Empty;

    public Manager(string name)
        {
            Name = name;
        }

}