
namespace Example.StudentsManagement.Models
{
    public class Administrator
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ApplicationUser User { get; set; }
    }
}