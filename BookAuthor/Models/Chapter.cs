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
        
        [Display(Name = "Название главы:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Введите наименование главы:")]
        public string Name { get; set; }
        
        [Display(Name = "Краткое писание:")]
        [StringLength(200)]
        [Required(ErrorMessage = "Введите описание:")]
        public string Description { get; set; }
        
        [Display(Name = "Автор:")]
        [StringLength(120)]
        [Required(ErrorMessage = "Введите имя автора:")]
        public string Author { get; set; }

        [Display(Name = "Содержание:")]
        public string Text { get; set; }     

    }
}
