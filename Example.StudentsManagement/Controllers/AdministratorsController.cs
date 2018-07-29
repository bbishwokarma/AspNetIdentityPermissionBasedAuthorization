using System.Net;
using System.Web.Mvc;
using Example.StudentsManagement.Models;
using Example.StudentsManagement.DAL;
using System.Linq;
using AspNetMvc.Authorization.PermissionBased;
using Example.StudentsManagement.Models.Constants;

namespace Example.StudentsManagement.Controllers
{
    [Authorize]
    public class AdministratorsController : Controller
    {
        private InMemoryRepository db = new InMemoryRepository();

        [AuthorizePermission(new string[] { AppPermissions.VIEW_ADMINISTRATOR_PROFILES, AppPermissions.MANAGE_ADMINISTRATOR_PROFILE })]
        public ActionResult Index()
        {
            return View(db.GetAll<Administrator>());
        }


        [AuthorizePermission(new string[] { AppPermissions.VIEW_ADMINISTRATOR_PROFILES, AppPermissions.VIEW_OWN_ADMIN_PROFILE, AppPermissions.MANAGE_ADMINISTRATOR_PROFILE }, IdParameterName = "administratorId", ResourceType = ResourceTypes.ADMINISTRATOR)]
        public ActionResult Details(int? administratorId)
        {
            var identity = User.Identity;
            if (administratorId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.GetAll<Administrator>().Where(a => a.Id == administratorId.Value).FirstOrDefault();
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }


        [AuthorizePermission(AppPermissions.MANAGE_ADMINISTRATOR_PROFILE)]
        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizePermission(AppPermissions.MANAGE_ADMINISTRATOR_PROFILE)]
        public ActionResult Create([Bind(Include = "Id,Name")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                administrator.Id = db.GetAll<Administrator>().Select(a => a.Id).Max() + 1;
                db.Add(administrator);
                return RedirectToAction("Index");
            }

            return View(administrator);
        }


        [AuthorizePermission(new string[] { AppPermissions.MANAGE_ADMINISTRATOR_PROFILE })]
        public ActionResult Edit(int? administratorId)
        {
            if (administratorId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.GetAll<Administrator>().Where(a => a.Id == administratorId).FirstOrDefault();
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [AuthorizePermission(new string[] { AppPermissions.MANAGE_ADMINISTRATOR_PROFILE })]
        public ActionResult Edit([Bind(Include = "Id,Name")] Administrator administrator)
        {
            if (ModelState.IsValid)
            {
                var admin = db.GetAll<Administrator>().FirstOrDefault(a => a.Id == administrator.Id);
                if (admin != null)
                {
                    admin.Name = administrator.Name;
                }
                return RedirectToAction("Index");
            }
            return View(administrator);
        }


        [AuthorizePermission(AppPermissions.MANAGE_ADMINISTRATOR_PROFILE)]
        public ActionResult Delete(int? administratorId)
        {
            if (administratorId == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Administrator administrator = db.GetAll<Administrator>().FirstOrDefault(a => a.Id == administratorId);
            if (administrator == null)
            {
                return HttpNotFound();
            }
            return View(administrator);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [AuthorizePermission(AppPermissions.MANAGE_ADMINISTRATOR_PROFILE)]
        public ActionResult DeleteConfirmed(int administratorId)
        {
            Administrator administrator = db.GetAll<Administrator>().FirstOrDefault(a => a.Id == administratorId);
            db.Remove(administrator);
            return RedirectToAction("Index");
        }


        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
    }
}
