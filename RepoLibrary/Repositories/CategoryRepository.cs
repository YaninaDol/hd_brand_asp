using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;
using Microsoft.EntityFrameworkCore;

namespace RepoLibrary.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRep
    {
        public CategoryRepository(HdBrandDboContext context) : base(context)

        {

        }

        List<SubCategory> ICategoryRep.GetSubCategoryNamesByCategoryId(int categoryId)
        {
            var subCategories = db.Products
       .Where(p => p.Categoryid == categoryId && p.SubCategoryid != null)
       .Select(p => p.SubCategoryid)
       .Distinct()
       .ToList();

           
            var subCategoryIds = subCategories.Select(s => Convert.ToInt32(s)).ToList();

          
            var resultSubCategories = db.SubCategories
                .Where(s => subCategoryIds.Contains(s.Id))
                .ToList();

            return resultSubCategories;
        }

        List<Material> ICategoryRep.MaterialNamesByCategoryId(int categoryId)
        {
            var materialsIds = db.Products
       .Where(p => p.Categoryid == categoryId && p.Materialid != null)
       .Select(p => p.Materialid)
       .Distinct()
       .ToList();

          
            var materials = db.Materials
                .Where(s => materialsIds.Contains(s.Id))
                .ToList();

            return materials;
        }
    }
}
