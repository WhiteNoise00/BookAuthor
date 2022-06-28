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
        
        [Display(Name = "Название события:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Введите наименование события:")]
        public string Name { get; set; }
        
        [Display(Name = "Краткое писание:")]
        [StringLength(200)]
        [Required(ErrorMessage = "Введите описание события:")]
        public string Description { get; set; }

    }
}
