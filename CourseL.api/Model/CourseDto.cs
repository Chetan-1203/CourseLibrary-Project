using CourseL.api.Entities;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace CourseL.api.Model
{
    public class CourseDto
    {
       
        public Guid Id { get; set; }

        
        public string Title { get; set; }

        
        public string Description { get; set; }

   
        public Guid AuthorId { get; set; }
    }
}
