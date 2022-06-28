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

        [Display(Name = "Наименование заметки:")]
        [Required(ErrorMessage = "Имя не должно быть пустым и превышать 120 символов.")]
        [MaxLength(120)]
        public string Name { get; set; }

        [Display(Name = "Содержание:")]
        [Required(ErrorMessage = "Содержание не должно быть пустым и превышать 3000 символов.")]
        [MaxLength(3000)]
        public string Text{ get; set; }

    }
}
