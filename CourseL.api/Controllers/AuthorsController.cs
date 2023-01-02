using AutoMapper;
using CourseL.api.Entities;
using CourseL.api.Helpers;
using CourseL.api.Model;
using CourseL.api.ResourceParameters;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace CourseL.api.Controllers
{
    [ApiController]
    [Route("api/authors")]
    [ApiExplorerSettings(GroupName = "CourseLibraryAPIAuthors")]
    public class AuthorsController : ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public AuthorsController(ICourseLibraryRepository courseLibraryRepository ,IMapper mapper)
        {
                _courseLibraryRepository= courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));  
                _mapper= mapper ?? throw new ArgumentNullException(nameof(mapper)); 
        
        }
        /// <summary>
        /// Get authors list by searching 
        /// </summary>
        /// <param name="authorsResourceParameters"> It takes two parameters main category and search query</param>
        /// <returns></returns>
        
        [HttpGet()]
        [HttpHead]
       
        
        public ActionResult<IEnumerable<AuthorDto>> GetAuthors([FromQuery]AuthorsResourceParameters authorsResourceParameters)
        {
            var authorsFromRepo = _courseLibraryRepository.GetAuthors(authorsResourceParameters);
            return Ok(_mapper.Map<IEnumerable<AuthorDto>>(authorsFromRepo));
        }
        
        /// <summary>
        /// Get author details for a specific author id
        /// </summary>
        /// <param name="authorId">passing author id from uri</param>
        /// <returns> It return response if it finds the id</returns>
        /// <response code ="200">Returns a requested author</response>
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

        
        /// <summary>
        /// creates a new author
        /// </summary>
        /// <param name="author"></param>
        /// <returns></returns>
        /// 
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

        /// <summary>
        /// Delete a author
        /// </summary>
        /// <param name="AuthorId">It takes author id as a parameter</param>
        /// <returns></returns>
        [HttpDelete("{authorId}")]

        public ActionResult DeleteAuthor(Guid AuthorId)
        {
            var authorFromRepo = _courseLibraryRepository.GetAuthor(AuthorId);

            if (authorFromRepo == null)
            {
                return NotFound();
            }
            _courseLibraryRepository.DeleteAuthor(authorFromRepo);
            _courseLibraryRepository.Save();

            return NoContent();
        }
    }
}
