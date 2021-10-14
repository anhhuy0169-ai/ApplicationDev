using ApplicationDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationDev.ViewModel
{
    public class AssignTraineetoCourseViewModel
    {
        public AssignTraineeToCourse AssignTraineetoCourse { get; set; }
        public IEnumerable<ApplicationUser> Trainees { get; set; }
        public IEnumerable<Course> Courses { get; set; }
    }
}