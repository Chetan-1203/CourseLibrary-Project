using AutoMapper;
using CourseL.api.Model;
using CourseLibrary.API.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace CourseL.api.Controllers
{
    [ApiController]
    [Route("api/authorcollection")]
    public class AuthorCollectionController :ControllerBase
    {
        private readonly ICourseLibraryRepository _courseLibraryRepository;
        private readonly IMapper _mapper;
        public AuthorCollectionController(ICourseLibraryRepository courseLibraryRepository, IMapper mapper)
        {
            _courseLibraryRepository = courseLibraryRepository ?? throw new ArgumentNullException(nameof(courseLibraryRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        }
       



        [HttpPost]
        public ActionResult<IEnumerable<AuthorDto>> CreateAuthorCollection(IEnumerable<AuthorForCreationDto> authorCollection)
        {
            var authorEntities = _mapper.Map<IEnumerable<Entities.Author>>(authorCollection);
            foreach (var author in authorEntities)
            {
                _courseLibraryRepository.AddAuthor(author);
            }
            _courseLibraryRepository.Save();

            return Ok();

        }
    }
}
