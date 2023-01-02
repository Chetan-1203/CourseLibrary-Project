using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using MongoDB.Bson;

using MongoDB.Bson.Serialization.Attributes;

namespace CourseL.api.Entities
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
       
        [Required]
        [MaxLength(50)]
        
        public string FirstName { get; set; }
        
        [Required]
        [MaxLength(50)]
      
        public string LastName { get; set; }
        /// <summary>
        /// The date of birth of author
        /// </summary>
        [Required]

        public DateTimeOffset DateOfBirth { get; set; }          
        /// <summary>
        /// 
        /// </summary>
        [Required]
        [MaxLength(50)]
    
        public string MainCategory { get; set; }

       
        public ICollection<Course> Courses { get; set; }
            = new List<Course>();
    }
}
