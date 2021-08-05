using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.Models
{
    public class MyDBContext :DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext>options):base(options)
        {

        }
        public DbSet<Book> Books { get; set; }
        public DbSet<Auther>Authers  { get; set; }
    }
}
