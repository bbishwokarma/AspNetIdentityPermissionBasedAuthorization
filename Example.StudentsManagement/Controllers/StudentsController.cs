using System.Linq;
using System.Net;
using System.Web.Mvc;
using Example.StudentsManagement.Models;
using Example.StudentsManagement.DAL;
using AspNetMvc.Authorization.PermissionBased;
using Example.StudentsManagement.Models.Constants;

namespace Example.StudentsManagement.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {
        private InMemoryRepository db = new InMemoryRepository();


        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE, AppPermissions.VIEW_STUDENT_PROFILES })]
        public ActionResult Index()
        {
            return View(db.GetAll<Student>());
        }


        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE, AppPermissions.VIEW_STUDENT_PROFILES, AppPermissions.VIEW_OWN_STUDENT_PROFILE }, IdParameterName = "studentId", ResourceType = ResourceTypes.STUDENT)]
        public ActionResult Details(int? studentId)
        {
            if (studentId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.GetAll<Student>().FirstOrDefault(s => s.Id == studentId);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE })]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE })]
        public ActionResult Create([Bind(Include = "Id,Name,EnrollmentDate,DateOfBirth,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                student.Id = db.GetAll<Student>().Max(s => s.Id) + 1;
                db.Add(student);
                return RedirectToAction("Index");
            }

            return View(student);
        }


        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE })]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.GetAll<Student>().FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE })]
        public ActionResult Edit([Bind(Include = "Id,Name,EnrollmentDate,DateOfBirth,Address")] Student student)
        {
            if (ModelState.IsValid)
            {
                Student s = db.GetAll<Student>().FirstOrDefault(s1 => s1.Id == student.Id);
                if (s != null)
                {
                    s.Name = student.Name;
                    s.EnrollmentDate = student.EnrollmentDate;
                    s.DateOfBirth = student.DateOfBirth;
                    s.Address = student.Address;
                }
                return RedirectToAction("Index");
            }
            return View(student);
        }


        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE })]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.GetAll<Student>().FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                return HttpNotFound();
            }
            return View(student);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizePermission(new string[] { AppPermissions.MANAGE_STUDENT_PROFILE })]
        public ActionResult DeleteConfirmed(int id)
        {
            Student student = db.GetAll<Student>().FirstOrDefault(s => s.Id == id);
            db.Remove(student);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
