using ApplicationDev.Models;
using ApplicationDev.Utils;
using ApplicationDev.ViewModel;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web.Mvc;

namespace ApplicationDev.Controllers
{
	[Authorize(Roles = Role.Manager)]
	public class StaffViewModelsController : Controller
	{
		ApplicationDbContext _context;
		public StaffViewModelsController()
		{
			_context = new ApplicationDbContext();
		}
		// GET: StaffViewModels
		public ActionResult Index()
		{
			var traineeRole = (from te in _context.Roles where te.Name.Contains("Trainee") select te).FirstOrDefault();
			// Get User role name Trainee and return
			var traineeUser = _context.Users.Where(u => u.Roles.Select(teus => teus.RoleId).Contains(traineeRole.Id)).ToList();
			// Get the user in the User table roles as Trainee and return list
			var traineeUserVM = traineeUser.Select(user => new StaffViewModel
			// return list out to VM
			{
				UserName = user.UserName,
				Email = user.Email,
				RoleName = "Trainee",
				UserID = user.Id
			}).ToList();
			var staff = new StaffViewModel { Trainee = traineeUserVM /*, Trainer = trainerUserVM */};
			return View(staff);
		}

		[HttpGet]
		public ActionResult Edit(string id)
		{

			var editUser = _context.Users.Find(id);
			if (editUser == null)
			{
				return HttpNotFound();
			}
			return View(editUser);
		}

		[HttpPost]
		public ActionResult Edit(ApplicationUser user)
		{
			var userInDb = _context.Users.Find(user.Id);

			if (userInDb == null)
			{
				return View(user);
			}

			if (ModelState.IsValid)
			{

				//userInDb.PhoneNumber = user.PhoneNumber;
				userInDb.Email = user.Email;

				_context.Users.AddOrUpdate(userInDb);
				_context.SaveChanges();

				return RedirectToAction("Index");
			}
			return View(user);

		}

		public ActionResult Delete(string id)
		{
			var userInDb = _context.Users.SingleOrDefault(p => p.Id == id);

			if (userInDb == null)
			{
				return HttpNotFound();
			}
			_context.Users.Remove(userInDb);
			_context.SaveChanges();

			return RedirectToAction("Index");
		}
	}
}