using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class ColorRepository : GenericRepository<Color>, IColorRep
    {
        public ColorRepository(HdBrandDboContext context) : base(context)

        {

        }

     
    }
}
