using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApplicationDev.Models
{
    public class Trainee
    {
        public int ID { get; set; }
        [Required]
        public string TraineeID { get; set; }
        public string Full_Name { get; set; }
        public string Email { get; set; }
        public string Age { get; set; }
        public string DateOfBirth { get; set; }
        public string Education { get; set; }
        //public int Phone { get; set; }
        public ApplicationUser Trainees { get; set; }
        //public bool isVerified { get; set; }
    }
}