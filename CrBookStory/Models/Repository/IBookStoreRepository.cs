using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CrBookStory.Models.Repository
{
    public interface IBookStoreRepository<TEntity> where TEntity:class
    {
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> GetAllWitheInclude(string str);
        TEntity Find(int id);
        TEntity FindWithInclude(int id, string str);

        void Insert(TEntity entity);
        void Update(int id,TEntity entity);
        void Delete(int id);
        void Save();
        IEnumerable<TEntity> Search(string term);
    }
}
