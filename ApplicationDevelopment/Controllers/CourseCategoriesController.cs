using ApplicationDevelopment.Models;
using System;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FPT_Management_System.Controllers
{

    public class CourseCategoriesController : Controller
    {
        public ApplicationDbContext _context;

        public CourseCategoriesController()
        {
            _context = new ApplicationDbContext();
        }

        //Categories/Index
        [HttpGet]
        public ActionResult Index(string searchCategory)
        {
            var categoryInDb = _context.CourseCategories.ToList();

            if (!String.IsNullOrEmpty(searchCategory))
            {
                categoryInDb = categoryInDb.FindAll(s => s.Name.Contains(searchCategory));
            }

            return View(categoryInDb);
        }

        //Categories/Create
        [HttpGet]
        public ActionResult CreateCategory()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CreateCategory(CourseCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View("CreateCategory");
            }
            //check category
            var check = _context.CourseCategories.Any(
                c => c.Name.Contains(category.Name));
            if (check)
            {
                ModelState.AddModelError("", "Category Already Exists.");
                return View("CreateCategory");
            }

            var newCategory = new CourseCategory()
            {
                Name = category.Name,
                Description = category.Description,
            };
            _context.CourseCategories.Add(category);
            _context.SaveChanges();
            return RedirectToAction("Index", "CourseCategories");
        }

        [HttpGet]
        public ActionResult EditCategory(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var categoryInDb = _context.CourseCategories.SingleOrDefault(t => t.Id == id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }
            return View(categoryInDb);
        }

        [HttpPost]
        public ActionResult EditCategory(CourseCategory category)
        {
            if (!ModelState.IsValid)
            {
                return View(category);
            }
            var categoryInDb = _context.CourseCategories.SingleOrDefault(
                t => t.Id == category.Id);
            if (categoryInDb == null)
            {
                return HttpNotFound();
            }

            var check = _context.CourseCategories.Any(
                c => c.Name.Contains(category.Name));
            if (check)
            {
                ModelState.AddModelError("", "Category Already Exists.");
                return View(category);
            }

            categoryInDb.Description = category.Description;
            categoryInDb.Name = category.Name;

            _context.SaveChanges();
            return RedirectToAction("Index", "CourseCategories");
        }

        //GET: Delete
        [HttpGet]
        public ActionResult DeleteCategory(int id)
        {
            var categoryInDb = _context.CourseCategories.SingleOrDefault(c => c.Id == id);

            if (categoryInDb == null)
            {
                return HttpNotFound();
            }

            _context.CourseCategories.Remove(categoryInDb);
            _context.SaveChanges();
            return RedirectToAction("Index", "CourseCategories");
        }
    }
}