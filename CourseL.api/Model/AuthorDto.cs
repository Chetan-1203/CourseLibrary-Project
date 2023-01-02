using System.ComponentModel.DataAnnotations;
using System;

namespace CourseL.api.Model
{
    public class AuthorDto
    {          /// <summary>
              /// The id of author
             /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The  name of author
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// age of an author
        /// </summary>
        public int Age { get; set; }

        public string MainCategory { get; set; }
    }
}
