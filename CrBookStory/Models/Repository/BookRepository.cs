using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.Models.Repository
{
    public class BookRepository : IBookStoreRepository<Book>
    {
        private MyDBContext db;
        
        public BookRepository(MyDBContext context)
        {

            db = context;
        }
        public void Delete(int id)
        {
            var book = Find(id);
            db.Books.Remove(book);
            
        }

        public Book Find(int id)
        {
            var book= db.Books.SingleOrDefault(a=>a.Id==id);
            return book;
        }

        public IEnumerable<Book> GetAll()
        {
            return db.Books.ToList();
        }

        public void Insert(Book entity)
        {
            
            db.Books.Add(entity);
        }

        public void Update(int id,Book newbook)
        {
            db.Update(newbook);
        }
        public void Save()
        {
            db.SaveChanges();
        }

        public IEnumerable<Book> GetAllWitheInclude(string str)
        {
            return db.Books.Include(str).ToList();
        }

        public Book FindWithInclude(int id,string str)
        {
            return db.Books.Include(str).SingleOrDefault(a => a.Id == id);
        }

        public IEnumerable<Book> Search(string term)
        {
            var srch= db.Books.Include(a => a.Auther).Where(b => b.Title.Contains(term) ||
              b.Description.Contains(term) || b.Auther.Name.Contains(term));
            return srch;

        }
    }
}
