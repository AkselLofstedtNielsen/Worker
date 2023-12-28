namespace WorkerPlatform.API.Models;


    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set;} = string.Empty;

        public ICollection<WorkField> WorkFields { get; set;} = new List<WorkField>();
        // public int WorkFieldId { get; set; } //FK

        
        public Employee(string name){
            Name = name;
        }

         public Employee()
        {
      
        }
    }


