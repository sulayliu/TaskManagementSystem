using Microsoft.AspNet.Identity;
using System;
using System.Net;
using System.Web.Mvc;
using TaskManagementSystem.Models;

namespace TaskManagementSystem.Controllers
{
    public class ProjectsController : Controller
    {
        // GET: Projects
        [Authorize]
        public ActionResult Index()
        {
            FilterViewModel filterModel = new FilterViewModel();
            ViewBag.SelectFilter = new SelectList(filterModel.FilterOptions);
            return View(ProjectHelper.GetProjectsByManager(User.Identity.GetUserId()));
        }

        [HttpPost]
        public ActionResult Index(string SelectFilter)
        {
            FilterViewModel filterModel = new FilterViewModel();
            ViewBag.SelectFilter = new SelectList(filterModel.FilterOptions);
            var projects = ProjectHelper.GetProjectsByManager(User.Identity.GetUserId());

            if (SelectFilter == "Creation Date")
            {
                projects = ProjectHelper.GetProjectsByManager(User.Identity.GetUserId());
            }
            else if (SelectFilter == "Completion Percentage")
            {
                projects = ProjectHelper.GetProjectsWithTaskOrderByPercent(User.Identity.GetUserId());
            }
            else if (SelectFilter == "Priority")
            {
                projects = ProjectHelper.GetProjectsWithTaskOrderByPriority(User.Identity.GetUserId());
            }

            return View(projects);
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
        public ActionResult Create(string name, string content, double budget, DateTime deadline, Priority priority)
        {
            if (ModelState.IsValid)
            {
                ProjectHelper.Create(User.Identity.GetUserId(), User.Identity.GetUserName(), name, content, budget, deadline, priority);
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
            Project project = ProjectHelper.GetProject((int)id);
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
        public ActionResult Edit([Bind(Include = "Id,name,content,CreatedTime,IsCompleted,ManagerId,budget,deadline,priority")] Project project)
        {
            if (ModelState.IsValid)
            {
                ProjectHelper.Edit(project.Id, project.Name, project.Content, project.IsCompleted, project.Budget, project.Deadline, project.Priority);
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
            Project project = ProjectHelper.GetProject((int)id);
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
            Project project = ProjectHelper.GetProject((int)id);
            ProjectHelper.Delete(project.Id);
            return RedirectToAction("Index");
        }

        //*****************************************
        // GET: Projects/Details/5
        public ActionResult Details(int id)
        {
            Project project = ProjectHelper.GetProject(id);
            if (project == null)
            {
                return HttpNotFound();
            }
            return View(project);
        }
        //Add an action method for projects with exceeded budgets
        public ActionResult GetExceededDeadlines()
        {
            return View(ProjectHelper.GetExceededDeadlines());
        }
        //Add an action method for projects with exceeded deadlines
        public ActionResult GetExceededBudgets()
        {
            return View("GetExceededDeadlines", ProjectHelper.GetExceededBudgets());
        }
    }
}
