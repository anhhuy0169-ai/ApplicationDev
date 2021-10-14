using ApplicationDev.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApplicationDev.ViewModel
{
    public class TrainerUserViewModel
    {
        public Trainer TrainerUsers { get; set; }
        public IEnumerable<ApplicationUser> Trainers { get; set; }
    }
}