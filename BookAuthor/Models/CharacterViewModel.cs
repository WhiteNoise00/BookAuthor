using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class CharacterViewModel
    {
        public int Id { get; set; }      

        [Display(Name = "Character`s name:")]
        [Required(ErrorMessage = "Enter character name:")]
        [StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Full character name:")]
        [Required(ErrorMessage = "Enter the full name of the character:")]
        [StringLength(200)]
        public string FullName { get; set; }

        [Display(Name = "Age:")]
        [Required(ErrorMessage = "Enter age:")]
        [StringLength(100)]
        public string Age { get; set; }

        [Display(Name = "Character gender:")]
        [Required(ErrorMessage = "Enter character gender:")] 
        public string Gender { get; set; }

        [Display(Name = "Character Image:")]
        public IFormFile Image { get; set; }

        [Display(Name = "Short description:")]
        [Required(ErrorMessage = "Short description:")]
        [StringLength(200)]
        public string Description { get; set; }

        [Display(Name = "Character history:")]
        [StringLength(3000)]
        public string History { get; set; }

        public string ImagePath { get; set; }
    }
}
