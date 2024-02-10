using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class MaterialRepository : GenericRepository<Material>, IMaterialRep
    {
        public MaterialRepository(HdBrandDboContext context) : base(context)

        {

        }

     
    }
}
