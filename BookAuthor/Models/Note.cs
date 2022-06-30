using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class Note
    {
        public int Id { get; set; }

        [Display(Name = "Note name:")]
        [Required(ErrorMessage = "The name must not be empty and must not exceed 120 characters:")]
        [MaxLength(120)]
        public string Name { get; set; }

        [Display(Name = "Content:")]
        [Required(ErrorMessage = "Content must not be empty and must not exceed 3000 characters:")]
        [MaxLength(3000)]
        public string Content{ get; set; }

    }
}
