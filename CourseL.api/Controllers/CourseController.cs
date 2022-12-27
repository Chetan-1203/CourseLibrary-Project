using AutoMapper;
using CourseL.api.Model;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseL.api.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public CourseController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var coursesforAuthorFromRepo =_courseLibraryRepository.GetCourses(authorId);   
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesforAuthorFromRepo));

        }

        [HttpGet("{courseId}")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId ,Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseforAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId,courseId);
            if (courseforAuthorFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(courseforAuthorFromRepo));

        }
    }
}
