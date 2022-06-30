using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class Chapter
    {
        public int Id { get; set; }
        
        [Display(Name = "Chapter title:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Enter the title of the chapter:")]
        public string Title { get; set; }
        
        [Display(Name = "Short description:")]
        [StringLength(200)]
        [Required(ErrorMessage = "Short description:")]
        public string Description { get; set; }
        
        [Display(Name = "Author:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Enter Author Name:")]
        public string Author { get; set; }

        [Display(Name = "Content:")]
        public string Content { get; set; }     

    }
}
