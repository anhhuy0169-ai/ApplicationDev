using ApplicationDev.Models;
using ApplicationDev.Utils;
using ApplicationDev.ViewModel;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace ApplicationDev.Controllers
{
	[Authorize(Roles = Role.Manager)]
	public class TrainerUsersController : Controller
	{
		private ApplicationDbContext _context;
		public TrainerUsersController()
		{
			_context = new ApplicationDbContext();
		}

		// GET: Trainees
		[HttpGet]

		public ActionResult Index()
		{
			if (User.IsInRole(Role.Manager))
			{
				var viewTrainer = _context.TrainerUsers.Include(a => a.Trainers).ToList();
				return View(viewTrainer);
			}
			if (User.IsInRole(Role.Trainer))
			{
				var trainerId = User.Identity.GetUserId();
				var trainerVM = _context.TrainerUsers.Where(te => te.TrainerID == trainerId).ToList();
				return View(trainerVM);
			}
			return View("Index");
		}

		[HttpGet]
		public ActionResult Create()
		{
			//Get Account Trainer
			var userInDb = (from r in _context.Roles where r.Name.Contains("Trainer") select r).FirstOrDefault();
			var users = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(userInDb.Id)).ToList();
			var trainerUser = _context.TrainerUsers.ToList();

			var viewModel = new TrainerUserViewModel
			{
				Trainers = users,
				TrainerUsers = new Trainer()
			};
			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Create(TrainerUserViewModel trainer)
		{
			var trainerinDb = (from te in _context.Roles where te.Name.Contains("Trainer") select te).FirstOrDefault();
			var trainerUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(trainerinDb.Id)).ToList();
			if (ModelState.IsValid)
			{

				var checkTrainerExist = _context.TrainerUsers.Include(t => t.Trainers).Where(t => t.Trainers.Id == trainer.TrainerUsers.TrainerID);
				if (checkTrainerExist.Count() > 0) //list ID comparison, if count == 0. jump to else
				{
					ModelState.AddModelError("", "Trainer Already Exists.");
				}
				else
				{
					_context.TrainerUsers.Add(trainer.TrainerUsers);
					_context.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			TrainerUserViewModel trainerUserView = new TrainerUserViewModel()
			{
				Trainers = trainerUser,
				TrainerUsers = trainer.TrainerUsers
			};
			return View(trainerUserView);
		}


		[HttpGet]
		public ActionResult Edit(int id)
		{
			var trainerInDb = _context.TrainerUsers.SingleOrDefault(tn => tn.ID == id);
			if (trainerInDb == null)
			{
				return HttpNotFound();
			}
			return View(trainerInDb);
		}

		[HttpPost]
		public ActionResult Edit(Trainer trainerUser)
		{
			if (!ModelState.IsValid)
			{
				return View();
			}
			var trainerInDb = _context.TrainerUsers.SingleOrDefault(tn => tn.ID == trainerUser.ID);
			if (trainerInDb == null)
			{
				return HttpNotFound();
			}
			trainerInDb.TrainerID = trainerUser.TrainerID;
			trainerInDb.Full_Name = trainerUser.Full_Name;
			trainerInDb.Age = trainerUser.Age;
			trainerInDb.Address = trainerUser.Address;
			trainerInDb.Specialty = trainerUser.Specialty;
			trainerInDb.Email = trainerUser.Email;

			

			_context.SaveChanges();
			return RedirectToAction("Index");
		}



		[HttpGet]
		public ActionResult Delete(int id)
		{
			var trainerInDb = _context.TrainerUsers.SingleOrDefault(tn => tn.ID == id);
			if (trainerInDb == null)
			{
				return HttpNotFound();
			}
			_context.TrainerUsers.Remove(trainerInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}