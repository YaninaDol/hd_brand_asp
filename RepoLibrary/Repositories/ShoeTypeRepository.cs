using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class ShoeTypeRepository : GenericRepository<ShoeType>, IShoeTypeRep
    {
        public ShoeTypeRepository(HdBrandDboContext context) : base(context)

        {

        }

     
    }
}
