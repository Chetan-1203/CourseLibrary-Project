using AutoMapper;

namespace CourseL.api.Profiles
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<Entities.Course, Model.CourseDto>();
        }
    }
}
