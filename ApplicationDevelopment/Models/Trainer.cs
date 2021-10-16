using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApplicationDevelopment.Models
{
    public class Trainer
    {
        [Key]
        [ForeignKey("User")]
        //khoa ngoai vs bang user
        public string TrainerId { get; set; }

        public ApplicationUser User { get; set; }

        //for validation
        [Required]

        public string FullName { get; set; }

        [Required]

        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        [Required]

        public string Specialty { get; set; }
    }
}