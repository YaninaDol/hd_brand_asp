using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRep
    {
        public CategoryRepository(HdBrandDboContext context) : base(context)

        {

        }

        public bool DeleteCategory( int Id)
        {

            if (db.Products.ToList().Where(x => x.Categoryid.Equals(Id)).Count() > 0 )
            {
                return false;
            }
            else
            {
                db.Categories.Remove(db.Categories.ToList().Where(x => x.Id.Equals(Id)).FirstOrDefault());
                return true;
            }

          
        }


       
        bool ICategoryRep.UpdateCategory(int id, string categoryName)
        {
            if (db.Categories.ToList().Any(x => x.Id == id))

            {

                db.Categories.ToList().Where((x) => x.Id.Equals(id)).FirstOrDefault().Name = categoryName;
                return true;



            }
            return false;
        }
    }
}
