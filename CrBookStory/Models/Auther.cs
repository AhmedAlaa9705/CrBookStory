using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.Models
{
    public class Auther
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50,MinimumLength =5)]
        public string Name { get; set; }
    }
}
