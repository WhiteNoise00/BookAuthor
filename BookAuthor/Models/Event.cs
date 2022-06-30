using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class Event
    {
        public int Id { get; set; }
        
        [Display(Name = "Event name:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Enter the name of the event:")]
        public string Name { get; set; }
        
        [Display(Name = "Short description:")]
        [StringLength(200)]
        [Required(ErrorMessage = "Enter a description for the event:")]
        public string Description { get; set; }

    }
}
