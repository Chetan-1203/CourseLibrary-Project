using AutoMapper;
using CourseL.api.Model;

namespace CourseL.api.Profiles
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<Entities.Course, Model.CourseDto>();
            CreateMap<Model.CourseForCreationForDto, Entities.Course>();
        }
    }
}
