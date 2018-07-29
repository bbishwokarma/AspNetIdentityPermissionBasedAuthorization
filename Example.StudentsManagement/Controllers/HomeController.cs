using Example.StudentsManagement.DAL;
using Example.StudentsManagement.Models;
using System.Linq;
using System.Web.Mvc;
using System.Web.Security;

namespace Example.StudentsManagement.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        InMemoryRepository db = new InMemoryRepository();
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Index(ApplicationUser user)
        {
            var existingUser = db.GetAll<ApplicationUser>().FirstOrDefault(u => u.Username == user.Username);
            if(existingUser == null)
            {
                ModelState.AddModelError("Username", "User does not exist");
                return View(user);
            }
            var student = db.GetAll<Student>().FirstOrDefault(s => s.User.Id == existingUser.Id);

           
            if (student != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Details", "Students", new { studentId = student.Id});
            }
            var administrator = db.GetAll<Administrator>().FirstOrDefault(a => a.User.Id == existingUser.Id);
            if(administrator != null)
            {
                FormsAuthentication.SetAuthCookie(user.Username, false);
                return RedirectToAction("Details", "Administrators", new { administratorId = administrator.Id });
            }
            ModelState.AddModelError("Username", "User not active");

            return View();
        }
    }
}