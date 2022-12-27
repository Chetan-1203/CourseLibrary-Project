using System.ComponentModel.DataAnnotations;
using System;

namespace CourseL.api.Model
{
    public class AuthorDto
    {
        public Guid Id { get; set; }

       
        public string Name { get; set; }

        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}
