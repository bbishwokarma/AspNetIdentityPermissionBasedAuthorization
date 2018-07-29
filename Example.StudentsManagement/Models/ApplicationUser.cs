using System.Collections.Generic;

namespace Example.StudentsManagement.Models
{
    public class ApplicationUser
    {
        public int Id { get; set; }
        public string Username { get; set; }

        /// <summary>
        /// Set of roles assigned to a user.
        /// Application should provide capability to assign or remove roles of a user.
        /// </summary>
        public ICollection<ApplicationRole> Roles { get; set; }

        public ApplicationUser()
        {
            Roles = new List<ApplicationRole>();
        }
    }
}