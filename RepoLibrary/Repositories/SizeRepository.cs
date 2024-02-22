using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class SizeRepository : GenericRepository<Size>, ISizesRep
    {
        public SizeRepository(HdBrandDboContext context) : base(context)

        {

        }

        List<Season> ISizesRep.GetSeasons => db.Seasons.ToList();
    }
}
