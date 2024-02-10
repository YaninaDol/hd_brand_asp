using hd_brand_asp.Data;
using Microsoft.EntityFrameworkCore;
using RepoLibrary.Interfaces;

namespace RepoLibrary.Repositories
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
       readonly protected HdBrandDboContext db;
      
        public GenericRepository(HdBrandDboContext context)
        {
            this.db = context;
           
        }
     

        public void Create(T item) => db.Set<T>().Add(item); 
       

        public bool Delete(int id)
        {
             db.Set<T>().Remove(db.Set<T>().Find(id));
            return true;
        }

        public T Get(int id) 
        {
          

            return db.Set<T>().Find(id);
            
        }

        public IEnumerable<T> GetAll()=>db.Set<T>().ToList();

        public void Update(T entity)
        {
            db.Entry(entity).State = EntityState.Modified;
        }
    }
}
