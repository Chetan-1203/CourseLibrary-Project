using AutoMapper;
using CourseL.api.Model;

namespace CourseL.api.Profiles
{
    public class CourseProfile:Profile
    {
        public CourseProfile()
        {
            CreateMap<Entities.Course, Model.CourseDto>();
            CreateMap<Model.CourseForCreationDto, Entities.Course>();
            CreateMap<Model.CourseForUpdateDto, Entities.Course>();
            CreateMap<Entities.Course ,Model.CourseForUpdateDto>(); 
        }
    }
}
