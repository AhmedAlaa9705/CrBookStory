using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.Models.Repository
{
    public class AutherRepository : IBookStoreRepository<Auther>
    {
        private MyDBContext db;
        public AutherRepository( MyDBContext context)
        {

            db = context;
        }
        public void Delete(int id)
        {
            var auther = Find(id);
            db.Authers.Remove(auther);
        }

        public Auther Find(int id)
        {
            var auther= db.Authers.SingleOrDefault(a => a.Id == id);
            return auther;
        }

        public IEnumerable<Auther> GetAll()
        {
            return db.Authers.ToList();
        }

        public void Insert(Auther entity)
        {
           
            db.Authers.Add(entity);
        }

        public void Update(int id, Auther newauther)
        {

            db.Update(newauther);

        }
        public void Save()
        {

            db.SaveChanges();

        }

        public IEnumerable<Auther> GetAllWitheInclude(string str)
        {
            throw new NotImplementedException();
        }

        public Auther FindWithInclude(int id, string str)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Auther> Search(string term)
        {
            throw new NotImplementedException();
        }
    }
}
