using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ApplicationDev.Models
{
	public class Trainer
	{

		public int ID { get; set; }
		[Required]
		public string TrainerID { get; set; }
		[StringLength(255)]
		public string Full_Name { get; set; }
		[StringLength(255)]
		public string Specialty { get; set; }
		[StringLength(255)]
		public string Email { get; set; }
		public string Age { get; set; }
			[StringLength(255)]
		public string Address { get; set; }




		public ApplicationUser Trainers { get; set; }

	}
}