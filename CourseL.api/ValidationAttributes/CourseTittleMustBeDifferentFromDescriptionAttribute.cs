using CourseL.api.Model;
using System.ComponentModel.DataAnnotations;

namespace CourseL.api.ValidationAttributes
{
    public class CourseTittleMustBeDifferentFromDescriptionAttribute : ValidationAttribute
    {

        protected override ValidationResult IsValid(object value , ValidationContext validationContext)
        {
            var course = (CourseForManipulationDto)validationContext.ObjectInstance;

            if(course.Title == course.Description)
            {
                 return new ValidationResult("The provided description should be different from a title.",
                   new[] { nameof(CourseForManipulationDto) });
            }

            return ValidationResult.Success;
        }
    }
}
