using AutoMapper;
using CourseL.api.Helpers;
using CourseL.api.Model;
using CourseL.api.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseL.api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository ,IMapper mapper)
        {
                _courseLibraryRepository= courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));  
                _mapper= mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        
        }

        [HttpGet()]
        [HttpHead]
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery]AuthorsResourceParameters authorsResourceParameters)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }
        [HttpGet("{authorId:guid}", Name ="GetAuthor")]
        public IActionResult GetAuthor(Guid authorId)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthor(authorId);
            if (authorsFromRepo==null)
            {
                return NotFound();
            }

           
            return Ok(_mapper.Map<AuthorDto>(authorsFromRepo));
        }
        [HttpPost]
        public ActionResult<AuthorDto> CreateAuthor(AuthorForCreationDto author)
        {
            if(author==null)
            {
                return BadRequest();
            }
            var authorEntity = _mapper.Map<Entities.Author>(author);
            _courseLibraryRepository.AddAuthor(authorEntity);
            _courseLibraryRepository.Save();

            var authorToReturn = _mapper.Map<AuthorDto>(authorEntity);
            return CreatedAtRoute("GetAuthor" , new {authorId=authorToReturn.Id},authorToReturn);
        }
    }
}
