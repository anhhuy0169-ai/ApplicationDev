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
	public class AssignTrainertoCoursesController : Controller
	{
		private ApplicationDbContext _context;
		public AssignTrainertoCoursesController()
		{
			_context = new ApplicationDbContext();
		}

		[HttpGet]
		// GET: AssignTraineetoCourses
		public ActionResult Index()
		{
			if (User.IsInRole(Role.Manager))
			{
				var viewAssign = _context.AssignTrainertoCourses.Include(a => a.Course).Include(a => a.Trainer).ToList();
				return View(viewAssign);
			}
			if (User.IsInRole(Role.Trainer))
			{
				var traineeId = User.Identity.GetUserId();
				var traineeVM = _context.AssignTrainertoCourses.Where(te => te.TrainerID == traineeId).Include(te => te.Course).ToList();
				return View(traineeVM);
			}
			return View();
		}

		//GET: Trainer and Course
		[HttpGet]
		public ActionResult Create()
		{
			var trainerInDb = (from te in _context.Roles where te.Name.Contains("Trainer") select te).FirstOrDefault();
			// Get User role name Trainee and return
			var trainerUser = _context.Users.Where(x => x.Roles.Select(y => y.RoleId).Contains(trainerInDb.Id)).ToList();
			// Get User in table and select the ID containing the TraineeID
			var courses = _context.Courses.ToList();

			var viewModel = new AssignTrainertoCourseViewModel
			{
				Courses = courses,
				Trainers = trainerUser,
				AssignTrainertoCourse = new AssignTrainerToCourse()
			};
			return View(viewModel);
		}


		[HttpPost]
		public ActionResult Create(AssignTrainertoCourseViewModel assign)
		{
			var trainerInDb = (from te in _context.Roles where te.Name.Contains("Trainer") select te).FirstOrDefault();
			var trainerUser = _context.Users.Where(u => u.Roles.Select(us => us.RoleId).Contains(trainerInDb.Id)).ToList();
			var course = _context.Courses.ToList();

			if (ModelState.IsValid)
			{
				var checkTrainerAndCourseExist = _context.AssignTrainertoCourses.Include(t => t.Course).Include(t => t.Trainer)
					.Where(t => t.Course.ID == assign.AssignTrainertoCourse.CourseID && t.Trainer.Id == assign.AssignTrainertoCourse.TrainerID);
				//GET CourseID and TraineeID from the Course and Trainee tables in the ViewModel

				if (checkTrainerAndCourseExist.Count() > 0) //list ID comparison, if count == 0. jump to else
				{
					ModelState.AddModelError("", "Assign Already Exists");
				}
				else
				{
					_context.AssignTrainertoCourses.Add(assign.AssignTrainertoCourse);
					_context.SaveChanges();
					return RedirectToAction("Index");
				}
			}
			AssignTrainertoCourseViewModel trainercourseVM = new AssignTrainertoCourseViewModel()
			{
				Courses = course,
				Trainers = trainerUser,
				AssignTrainertoCourse = assign.AssignTrainertoCourse
			};
			return View(trainercourseVM);
		}


		[HttpGet]
		public ActionResult Delete(int id)
		{
			var assignInDb = _context.AssignTrainertoCourses.SingleOrDefault(a => a.ID == id);
			if (assignInDb == null)
			{
				return HttpNotFound();
			}

			_context.AssignTrainertoCourses.Remove(assignInDb);
			_context.SaveChanges();
			return RedirectToAction("Index");
		}
	}
}