using Microsoft.AspNet.Identity;
using System.Net;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class ProjectsController : Controller
    {
        private ProjectHelper projectHelper = new ProjectHelper();

        // GET: Projects
        [Authorize]
        public ActionResult Index()
        {
            return View(projectHelper.GetProjectsWithTaskOrderByPercent());
        }

        // GET: Projects/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Projects/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(string Name, string Content)
        {
            if (ModelState.IsValid)
            {
                projectHelper.Create(User.Identity.GetUserId(), User.Identity.GetUserName(), Name, Content);
                return RedirectToAction("Index");
            }

            return View();
        }

        // GET: Projects/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = projectHelper.GetProject((int)id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Content,CreatedTime,IsCompleted,ManagerId")] Project project)
        {
            if (ModelState.IsValid)
            {
                projectHelper.Edit(project.Id, project.Name, project.Content, project.IsCompleted);
                return RedirectToAction("Index");
            }
            return View(project);
        }

        // GET: Projects/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Project project = projectHelper.GetProject((int)id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }

        // POST: Projects/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Project project = projectHelper.GetProject((int)id);
            projectHelper.Delete(project.Id);
            return RedirectToAction("Index");
        }
    }
}
