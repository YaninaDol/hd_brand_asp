using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class ProductssizeRepository : GenericRepository<Productssize>, IProductssize
    {
        public ProductssizeRepository(HdBrandDboContext context) : base(context)

        {

        }

        void IProductssize.DeleteAll(int id)
        {
           db.Productssizes.RemoveRange(db.Productssizes.Where(x => x.Productid == id));
        }

        IEnumerable<Productssize> IProductssize.getallbyid(int id)
        {
            return db.Productssizes.Where((x) => x.Productid.Equals(id)).ToList();
        }
    }
}
