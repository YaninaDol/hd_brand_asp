
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(HdBrandDboContext context) : base(context)
        {

        }


        public IEnumerable<Product> getAllList()
        {
            return db.Products.ToList();

        }

        public string getSide(int id)
        {
            if (db.Products.Any(x => x.Id.Equals(id)))
                return db.Products.ToList().Where(x => x.Id == id).FirstOrDefault().Name;
            else
                return "Null";
        }

        public bool Update(int IdUpdate, Product item)
        {
            if (db.Products.Any(x => x.Id.Equals(IdUpdate)))

            {

                db.Products.Where(x => x.Id == IdUpdate).FirstOrDefault().Name = item.Name;
               

                return true;

            }
            else return false;

        }

        bool IProductRepository.deleteSize(int Id, int Size)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getAllProducts()
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getByCategory(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getByMaterial(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getBySeason(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getByShoeType(int id)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getSizes(int idProduct)
        {
            throw new NotImplementedException();
        }
    }
}
