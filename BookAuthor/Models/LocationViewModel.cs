using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class LocationViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Location name:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Enter the name of the place:")]
        public string Name { get; set; }

        [Display(Name = "Short description:")]
        [StringLength(200)]
        [Required(ErrorMessage = "Enter a description:")]
        public string Description { get; set; }

        [Display(Name = "Location Image:")]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

    }
}
