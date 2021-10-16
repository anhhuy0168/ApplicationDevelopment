using ApplicationDevelopment.Models;
using ApplicationDevelopment.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace ApplicationDevelopment.Controllers
{
    public class AdminsController : Controller
    {
        //tao ket noi
        private ApplicationDbContext _context;

        private ApplicationUserManager _userManager;

        public AdminsController()
        {
            _context = new ApplicationDbContext();
        }

        public AdminsController(ApplicationUserManager userManager)
        {
            _context = new ApplicationDbContext();
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult IndexStaff()
        {
            var staff = _context.Staffs.ToList();
            var user = _context.Users.ToList();

            return View(staff);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateStaffAccount()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> CreateStaffAccount(StaffAccountViewModels viewModel)
        {
            if (!ModelState.IsValid)
            {
                var user = new ApplicationUser
                { UserName = viewModel.RegisterViewModels.Email, Email = viewModel.RegisterViewModels.Email };
                var result = await UserManager.CreateAsync(user, viewModel.RegisterViewModels.Password);
                var staffId = user.Id;
                var newStaff = new Staff()
                {
                    StaffId = staffId,
                    FullName = viewModel.Staffs.FullName,
                    Age = viewModel.Staffs.Age,
                    Address = viewModel.Staffs.Address,
                };

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "staff");
                    _context.Staffs.Add(newStaff);
                    _context.SaveChanges();
                }
                AddErrors(result);
            }

            return RedirectToAction("IndexStaff", "Admins");
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditStaffAccount(string id)
        {
            var staffInDb = _context.Staffs.SingleOrDefault(u => u.StaffId == id);
            if (staffInDb == null)
            {
                return HttpNotFound();
            }
            return View(staffInDb);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditStaffAccount(Staff staff)
        {
            if (!ModelState.IsValid)
            {
                return View(staff);
            }
            var staffInfoInDb = _context.Staffs.SingleOrDefault(t => t.StaffId == staff.StaffId);

            if (staffInfoInDb == null)
            {
                return HttpNotFound();
            }
            staffInfoInDb.FullName = staff.FullName;
            staffInfoInDb.Age = staff.Age;
            staffInfoInDb.Address = staff.Address;
            _context.SaveChanges();

            return RedirectToAction("IndexStaff", "Admins");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteStaffAccount(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            var staffInfoInDb = _context.Staffs.SingleOrDefault(i => i.StaffId == id);
            if (staffInDb == null || staffInfoInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(staffInDb);
            _context.Staffs.Remove(staffInfoInDb);
            _context.SaveChanges();
            return RedirectToAction("IndexStaff", "Admins");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult StaffInfoDetails(string id)
        {
            var staffId = User.Identity.GetUserId();

            var staffInfoInDb = _context.Staffs
                .SingleOrDefault(t => t.StaffId == id);

            if (staffInfoInDb == null)
            {
                return HttpNotFound();
            }
            return View(staffInfoInDb);
        }

        [Authorize(Roles = "admin")]
        public ActionResult StaffPasswordReset(string id)
        {
            var staffInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            if (staffInDb == null)
            {
                return HttpNotFound();
            }
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = staffInDb.Id;
            if (userId != null)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                string newPassword = "DefaultPassword@123";
                userManager.AddPassword(userId, newPassword);
            }
            _context.SaveChanges();
            return RedirectToAction("IndexStaff", "Admins");
        }

        /// <summary>
        /// FOR TRAINER
        /// </summary>

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult IndexTrainer()
        {
            var trainer = _context.Trainers.ToList();
            var user = _context.Users.ToList();

            return View(trainer);
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult CreateTrainerAccount()
        {
            return View();
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public async Task<ActionResult> CreateTrainerAccount(TrainerAccountViewModels viewModel)
        {
            if (!ModelState.IsValid)
            {
                var user = new ApplicationUser
                { UserName = viewModel.RegisterViewModels.Email, Email = viewModel.RegisterViewModels.Email };
                var result = await UserManager.CreateAsync(user, viewModel.RegisterViewModels.Password);
                var trainerId = user.Id;
                var newTrainer = new Trainer()
                {
                    TrainerId = trainerId,
                    FullName = viewModel.Trainers.FullName,
                    Age = viewModel.Trainers.Age,
                    Address = viewModel.Trainers.Address,
                    Specialty = viewModel.Trainers.Specialty
                };

                if (result.Succeeded)
                {
                    await UserManager.AddToRoleAsync(user.Id, "trainer");
                    _context.Trainers.Add(newTrainer);
                    _context.SaveChanges();
                }
                AddErrors(result);
            }

            return RedirectToAction("IndexTrainer", "Admins");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult EditTrainerAccount(string id)
        {
            var trainerInDb = _context.Trainers.SingleOrDefault(u => u.TrainerId == id);
            if (trainerInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInDb);
        }

        [Authorize(Roles = "admin")]
        [HttpPost]
        public ActionResult EditTrainerAccount(Trainer trainer)
        {
            if (!ModelState.IsValid)
            {
                return View(trainer);
            }
            var trainerInfoInDb = _context.Trainers.SingleOrDefault(t => t.TrainerId == trainer.TrainerId);

            if (trainerInfoInDb == null)
            {
                return HttpNotFound();
            }
            trainerInfoInDb.FullName = trainer.FullName;
            trainerInfoInDb.Age = trainer.Age;
            trainerInfoInDb.Address = trainer.Address;
            trainerInfoInDb.Specialty = trainer.Specialty;

            _context.SaveChanges();

            return RedirectToAction("IndexTrainer", "Admins");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult DeleteTrainerAccount(string id)
        {
            var trainerInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            var trainerInfoInDb = _context.Trainers.SingleOrDefault(i => i.TrainerId == id);
            if (trainerInDb == null || trainerInfoInDb == null)
            {
                return HttpNotFound();
            }
            _context.Users.Remove(trainerInDb);
            _context.Trainers.Remove(trainerInfoInDb);
            _context.SaveChanges();
            return RedirectToAction("IndexTrainer", "Admins");
        }

        [Authorize(Roles = "admin")]
        [HttpGet]
        public ActionResult TrainerInfoDetails(string id)
        {
            var trainerId = User.Identity.GetUserId();

            var trainerInfoInDb = _context.Trainers
                .SingleOrDefault(t => t.TrainerId == id);

            if (trainerInfoInDb == null)
            {
                return HttpNotFound();
            }
            return View(trainerInfoInDb);
        }

        [Authorize(Roles = "admin")]
        public ActionResult TrainerPasswordReset(string id)
        {
            var trainerInDb = _context.Users.SingleOrDefault(i => i.Id == id);
            if (trainerInDb == null)
            {
                return HttpNotFound();
            }
            var userId = System.Web.HttpContext.Current.User.Identity.GetUserId();
            userId = trainerInDb.Id;
            if (userId != null)
            {
                UserManager<IdentityUser> userManager = new UserManager<IdentityUser>(new UserStore<IdentityUser>());
                userManager.RemovePassword(userId);
                string newPassword = "DefaultPassword@123";
                userManager.AddPassword(userId, newPassword);
            }
            _context.SaveChanges();
            return RedirectToAction("IndexTrainer", "Admins");
        }
    }
}