using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class SceneViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Название места действия:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Введите наименование места:")]
        public string Name { get; set; }

        [Display(Name = "Краткое писание:")]
        [StringLength(200)]
        [Required(ErrorMessage = "Введите описание:")]
        public string Description { get; set; }

        [Display(Name = "Изображение места:")]
        public IFormFile Image { get; set; }
        public string ImagePath { get; set; }

    }
}
