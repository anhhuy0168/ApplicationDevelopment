using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ApplicationDevelopment.Models
{
    public class Course
    {
        [Key]
        public int CourseID { get; set; }

        [Required(ErrorMessage = "Please enter Course Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Please enter Course Description")]
        public string Description { get; set; }

        [ForeignKey("CourseCategory")]
        public int CategoryId { get; set; }

        public CourseCategory CourseCategory { get; set; }
    }
}