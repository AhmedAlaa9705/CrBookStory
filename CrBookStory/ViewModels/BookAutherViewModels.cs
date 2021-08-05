using CrBookStory.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.ViewModels
{
    public class BookAutherViewModels
    {
        public int BookId { get; set; }

        [Required]
        [StringLength(50,MinimumLength =5)]
        public string Title { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 5)]
        public string Description { get; set; }

        public int AutherId { get; set; }

        public IList<Auther> Authers { get; set; }
        public IFormFile File { get; set; }
        public string ImgUrl { get; set; }
    }
}
