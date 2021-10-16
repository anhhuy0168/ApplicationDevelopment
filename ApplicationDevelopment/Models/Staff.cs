using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ApplicationDevelopment.Models
{
    public class Staff
    {
        [Key]
        [ForeignKey("User")]
        public string StaffId { get; set; }

        public ApplicationUser User { get; set; }

        //for validation
        [Required]
       
        public string FullName { get; set; }

        [Required]
    
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }
    }
}