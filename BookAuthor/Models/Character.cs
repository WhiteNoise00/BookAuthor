using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookAuthor.Models
{
    public class Character
    {
        public int Id { get; set; }
    
        public string FullName { get; set; }
        public string Name { get; set; }

        public string Age { get; set; }
      
        public string Gender { get; set; }
    
        public string Description { get; set; }
               
        public string History { get; set; }

        public string PhotoPath { get; set; }
        public string PhotoName { get; set; }
    }
}
