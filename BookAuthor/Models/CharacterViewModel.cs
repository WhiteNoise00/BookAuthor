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

        [Display(Name = "Имя:")]
        [Required(ErrorMessage = "Введите имя персонажа:")]
        [StringLength(200)]
        public string Name { get; set; }

        [Display(Name = "Полное имя персонажа:")]
        [Required(ErrorMessage = "Введите полное имя персонажа:")]
        [StringLength(200)]
        public string FullName { get; set; }

        [Display(Name = "Возраст:")]
        [Required(ErrorMessage = "Введите возраст:")]
        [StringLength(100)]
        public string Age { get; set; }

        [Required(ErrorMessage = "Введите пол персонажа:")]
        [Display(Name = "Пол персонажа:")]
        public string Gender { get; set; }

        [Display(Name = "Изображение персонажа:")]
        public IFormFile Image { get; set; }

        [Display(Name = "Краткое описание:")]
        [Required(ErrorMessage = "Введите краткое описание:")]
        [StringLength(300)]
        public string Description { get; set; }

        [Display(Name = "История персонажа:")]
        [StringLength(3000)]
        public string History { get; set; }

        public string ImagePath { get; set; }
    }
}
