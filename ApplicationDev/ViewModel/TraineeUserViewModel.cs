using ApplicationDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationDev.ViewModel
{
    public class TraineeUserViewModel
    {
        public Trainee TraineeUser { get; set; }
        public IEnumerable<ApplicationUser> Trainees { get; set; }
    }
}