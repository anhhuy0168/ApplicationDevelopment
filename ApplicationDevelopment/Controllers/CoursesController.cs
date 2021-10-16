using ApplicationDevelopment.Models;
using ApplicationDevelopment.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ApplicationDevelopment.Controllers
{

    public class CoursesController : Controller
    {
        public ApplicationDbContext _context;

        public CoursesController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public ActionResult CourseIndex(string searchString)
        {
            var courses = _context.Courses
                .Include(t => t.CourseCategory)
                .ToList();

            if (!string.IsNullOrEmpty(searchString))
            {
                courses = courses
                    .Where(t => t.Description.ToLower().Contains(searchString.ToLower())
                            || t.CourseCategory.Description.ToLower().Contains(searchString.ToLower())
                ).ToList();
            }

            return View(courses);
        }

        [HttpGet]
        public ActionResult CourseCreate()
        {
            var categories = _context.CourseCategories.ToList();
            var viewModel = new CourseCategoriesViewModels()
            {
                CourseCategories = categories
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CourseCreate(CourseCategoriesViewModels model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CourseCategoriesViewModels
                {
                    Courses = model.Courses,
                    CourseCategories = _context.CourseCategories.ToList()
                };
                return View(viewModel);
            }

            var newCourse = new Course()
            {
                Name = model.Courses.Name,
                Description = model.Courses.Description,
                CategoryId = model.Courses.CategoryId,
            };

            _context.Courses.Add(newCourse);
            _context.SaveChanges();

            return RedirectToAction("CourseIndex", "Courses");
        }

        [HttpGet]
        public ActionResult CourseDelete(int id)
        {
            var courseInDb = _context.Courses
                .SingleOrDefault(t => t.CourseID == id);
            if (courseInDb == null)
            {
                return HttpNotFound();
            }

            _context.Courses.Remove(courseInDb);
            _context.SaveChanges();

            return RedirectToAction("CourseIndex", "Courses");
        }

        [HttpGet]
        public ActionResult CourseEdit(int id)
        {
            var courseInDb = _context.Courses
                .SingleOrDefault(t => t.CourseID == id);
            if (courseInDb == null)
            {
                return HttpNotFound();
            }

            var viewModel = new CourseCategoriesViewModels
            {
                Courses = courseInDb,
                CourseCategories = _context.CourseCategories.ToList()
            };
            return View(viewModel);
        }

        [HttpPost]
        public ActionResult CourseEdit(CourseCategoriesViewModels model)
        {
            if (!ModelState.IsValid)
            {
                var viewModel = new CourseCategoriesViewModels
                {
                    Courses = model.Courses,
                    CourseCategories = _context.CourseCategories.ToList()
                };
                return View(viewModel);
            }

            var courseInDb = _context.Courses
                .SingleOrDefault(t => t.CourseID == model.Courses.CourseID);
            if (courseInDb == null)
            {
                return HttpNotFound();
            }

            courseInDb.Name = model.Courses.Name;
            courseInDb.Description = model.Courses.Description;
            courseInDb.CategoryId = model.Courses.CategoryId;

            _context.SaveChanges();
            return RedirectToAction("CourseIndex", "Courses");
        }
    }
}