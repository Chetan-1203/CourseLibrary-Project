using System.ComponentModel.DataAnnotations;

namespace CourseL.api.Model
{
    public class CourseForUpdateDto :CourseForManipulationDto
    {
        [Required(ErrorMessage =" you must enter a description")]
        public override string Description { get => base.Description; set => base.Description = value; }
    }
}
