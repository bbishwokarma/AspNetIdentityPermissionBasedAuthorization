using System.Security.Principal;

namespace Example.StudentsManagement.Models
{
    public class MyIdentity : IIdentity
    {
        private string username;

        public MyIdentity(string username)
        {
            this.username = username;
        }

        public string Name => username;

        public string AuthenticationType =>"Form";

        public bool IsAuthenticated => true;
    }
}