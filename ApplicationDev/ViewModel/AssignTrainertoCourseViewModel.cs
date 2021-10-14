using ApplicationDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationDev.ViewModel
{
    public class AssignTrainertoCourseViewModel
    {
        public AssignTrainerToCourse AssignTrainertoCourse { get; set; }
        public IEnumerable<ApplicationUser> Trainers { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}