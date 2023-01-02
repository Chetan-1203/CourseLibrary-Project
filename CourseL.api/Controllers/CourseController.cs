using AutoMapper;
using CourseL.api.Entities;
using CourseL.api.Model;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseL.api.Controllers
{
    [ApiController]
    [Route("api/authors/{authorId}/courses")]
    [ApiExplorerSettings(GroupName = "CourseLibraryAPICourses")]
    public class CourseController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public CourseController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
        /// <summary>
        /// Get a courses for a specific author
        /// </summary>
        /// <param name="authorId">require author id </param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult<IEnumerable<CourseDto>> GetCoursesForAuthor(Guid authorId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var coursesforAuthorFromRepo = _courseLibraryRepository.GetCourses(authorId);
            return Ok(_mapper.Map<IEnumerable<CourseDto>>(coursesforAuthorFromRepo));

        }

        /// <summary>
        /// Get  specific course for a specific author 
        /// </summary>
        /// <param name="authorId">requires author id</param>
        /// <param name="courseId"> reuires course id</param>
        /// <returns></returns>

        [HttpGet("{courseId}", Name = "GetCourseForAuthor")]
        public ActionResult<CourseDto> GetCourseForAuthor(Guid authorId, Guid courseId)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseforAuthorFromRepo = _courseLibraryRepository.GetCourse(authorId, courseId);
            if (courseforAuthorFromRepo == null)
            {
                return NotFound();
            }
            return Ok(_mapper.Map<CourseDto>(courseforAuthorFromRepo));

        }
        /// <summary>
        /// creates a course for a specific author
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult<CourseDto> CreateCourseForAuthor(Guid authorId, CourseForCreationDto course)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseEntity = _mapper.Map<Entities.Course>(course);
            _courseLibraryRepository.AddCourse(authorId, courseEntity);
            _courseLibraryRepository.Save();

            var courseToReturn = _mapper.Map<CourseDto>(courseEntity);
            return CreatedAtRoute("GetCourseForAuthor", new { authorId = authorId, courseId = courseToReturn.Id }, courseToReturn);

        }

        /// <summary>
        /// This method updates a whole course
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="courseId"></param>
        /// <param name="course"></param>
        /// <returns></returns>
        [HttpPut("{courseId}")]
        public ActionResult UpdateCourseForAuthor(Guid authorId, Guid courseId, CourseForUpdateDto course)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }

            var courseForAuthorRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorRepo == null)
            {
                var courseToAdd = _mapper.Map<Entities.Course>(course);
                courseToAdd.Id = courseId;
                _courseLibraryRepository.AddCourse(authorId, courseToAdd);
                _courseLibraryRepository.Save();
                var courseToReturn = _mapper.Map<CourseDto>(courseToAdd);
                return CreatedAtRoute("GetCourseForAuthor", new { authorId, courseId = courseToReturn.Id }, courseToReturn);
            }
            _mapper.Map(course, courseForAuthorRepo);

            _courseLibraryRepository.UpdateCourse(courseForAuthorRepo);

            _courseLibraryRepository.Save();

            return NoContent();
        }

        /// <summary>
        /// This method updates course partiallly
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="courseId"></param>
        /// <param name="patchDocument"></param>
        /// <returns></returns>
        [HttpPatch("{courseId}")]
        public ActionResult PartiallyUpdatedCourseForAuthor(Guid authorId, Guid courseId,
            JsonPatchDocument<CourseForUpdateDto> patchDocument)
        {
            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                var courseDto = new CourseForUpdateDto();
                patchDocument.ApplyTo(courseDto);
                var courseToAdd = _mapper.Map<Entities.Course>(courseDto);
                courseToAdd.Id =courseId;
                _courseLibraryRepository.AddCourse(authorId, courseToAdd);
                _courseLibraryRepository.Save();
            }

            var courseForAuthorRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if (courseForAuthorRepo == null)
            {
                return NotFound();
            }
            var courseToPatch = _mapper.Map<CourseForUpdateDto>(courseForAuthorRepo);
            patchDocument.ApplyTo(courseToPatch);
          
            _mapper.Map(courseToPatch, courseForAuthorRepo);
            _courseLibraryRepository.UpdateCourse(courseForAuthorRepo);
            _courseLibraryRepository.Save();

            return NoContent();
        }
        /// <summary>
        /// Deletion of specific course for specific author
        /// </summary>
        /// <param name="authorId"></param>
        /// <param name="courseId"></param>
        /// <returns></returns>
        [HttpDelete("{courseId}")]

        public ActionResult DeleteCourseForAuthor(Guid authorId , Guid courseId)
        {

            if (!_courseLibraryRepository.AuthorExists(authorId))
            {
                return NotFound();
            }
            var courseForAuthorRepo = _courseLibraryRepository.GetCourse(authorId, courseId);

            if(courseForAuthorRepo == null)
            {
                return NotFound();
            }

            _courseLibraryRepository.DeleteCourse(courseForAuthorRepo);
            _courseLibraryRepository.Save();

            return NoContent();

        }
       
            
    }
}
