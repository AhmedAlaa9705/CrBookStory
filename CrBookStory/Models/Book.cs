using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AutherId { get; set; }
        
        public Auther Auther { get; set; }
        public string ImgUrl { get; set; }
    }
}
