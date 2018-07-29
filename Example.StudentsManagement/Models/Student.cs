using System;

namespace Example.StudentsManagement.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string Address { get; set; }
        
        public ApplicationUser User { get; set; }
    }
}