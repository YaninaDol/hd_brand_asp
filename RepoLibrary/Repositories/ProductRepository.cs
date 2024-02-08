
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(HdBrandDboContext context) : base(context)
        {

        }



        
        public bool Update(int IdUpdate, Product item)
        {
            if (db.Products.Any(x => x.Id.Equals(IdUpdate)))

            {

                db.Products.Where(x => x.Id == IdUpdate).FirstOrDefault().Name = item.Name;
               
                //add other fields
                return true;

            }
            else return false;

        }

        bool IProductRepository.deleteSize(int Id, int Size)
        {
            throw new NotImplementedException();
        }

        IEnumerable<Product> IProductRepository.getByCategory(int id)
        {
            if (db.Categories.Any(x => x.Id.Equals(id)))

            {


                //add other fields
                return db.Products.Where(x => x.Categoryid == id).ToList();

            }
            else return  db.Products.ToList();

        }

        IEnumerable<Product> IProductRepository.getByMaterial(int id)
        {
            if (db.Materials.Any(x => x.Id.Equals(id)))

            {

                return db.Products.Where(x => x.Materialid == id).ToList();

            }
            else return db.Products.ToList();
        }

        IEnumerable<Product> IProductRepository.getBySeason(int id)
        {
            if (db.Seasons.Any(x => x.Id.Equals(id)))

            {

                return db.Products.Where(x => x.Seasonid == id).ToList();

            }
            else return db.Products.ToList();
        }

        IEnumerable<Product> IProductRepository.getByShoeType(int id)
        {
            if (db.ShoeTypes.Any(x => x.Id.Equals(id)))

            {

                return db.Products.Where(x => Convert.ToInt32(x.ShoeType) == id).ToList();

            }
            else return db.Products.ToList();
        }

        IEnumerable<Product> IProductRepository.getProductBySize(int size)
        {
            if (db.Sizes.Any(x => x.Value.Equals(size)))

            {
                var result = from product in db.Products
                             join productSize in db.Productsizes on product.Id equals productSize.Productid
                             where productSize.Sizeid == size
                             select product;
                return result;


            }
            else return null;
        }
            IEnumerable<Productsize> IProductRepository.getSizes(int productId)
        {
            if (db.Products.Any(x => x.Id.Equals(productId)))

            {

                return db.Productsizes.Where(x => x.Productid == productId).ToList();

            }
            else return null;
        }
    }
}
