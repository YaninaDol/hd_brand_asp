using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;
using RepositoriesLibrary.Models;

namespace RepoLibrary.Repositories
{
    public class LikedProductRepository : GenericRepository<LikedProduct>, ILikedProductRep
    {
        public LikedProductRepository(HdBrandDboContext context) : base(context)

        {

        }

       

        public IEnumerable<Product> GetAllProducts(string userId)
        {
            var likedProducts = db.LikedProduct.Where(lp => lp.UserId == userId).Select(lp => lp.ProductId);

            var selectedProducts = db.Products.Where(p => likedProducts.Contains(p.Id)).ToList();
            return selectedProducts;
        }

        public bool getProduct(int prodId)
        {
            if (db.LikedProduct.Any((x)=> x.ProductId == prodId)) return true;
            else return false;
        }

      

        bool ILikedProductRep.setProduct(string userId, int prodId, bool like)
        {
            try
            {
                var item = db.LikedProduct.Where((x) => x.ProductId == prodId).FirstOrDefault();
                if (item != null)
                {
                    if (!like)
                    {
                        db.LikedProduct.Remove(item);
                    }
                }
                else db.LikedProduct.Add(new LikedProduct { UserId = userId, ProductId = prodId });
                return true;
            }
            catch { }
            return false;
        }
    }
}
