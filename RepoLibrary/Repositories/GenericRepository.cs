using hd_brand_asp.Data;
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
       

        public void Delete(int id)
        {
             db.Set<T>().Remove(db.Set<T>().Find(id));
        }

        public T Get(int id) 
        {
          

            return db.Set<T>().Find(id);
            
        }

        public IEnumerable<T> GetAll()=>db.Set<T>().ToList();

       
    }
}
