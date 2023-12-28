namespace WorkerPlatform.API.Models;
public class Manager
{
    public int Id { get; set; } //PK
    public string Name { get; set;} = string.Empty;

    public ICollection<WorkField> WorkFields {get; set;} = new List<WorkField>();

    public Manager(string name)
        {
            Name = name;
        }

        public Manager()
        {
      
        }

}