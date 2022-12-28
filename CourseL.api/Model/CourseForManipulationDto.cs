using CourseL.api.ValidationAttributes;
using System.ComponentModel.DataAnnotations;

namespace CourseL.api.Model
{
    [CourseTittleMustBeDifferentFromDescription(ErrorMessage ="Tite muste be differnent from description")]
    public abstract class CourseForManipulationDto
    {
        [Required(ErrorMessage = "You should fill out a title")]
        [MaxLength(100, ErrorMessage = "title must not exceed 100 characters")]
        public string Title { get; set; }

        [MaxLength(1500, ErrorMessage = "title must not exceed 1500 characters")]
        public virtual string Description { get; set; }
    }
}
