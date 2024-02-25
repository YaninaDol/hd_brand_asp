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
      

        Season ISizesRep.getSeasonById(int id)
        {
           return  db.Seasons.Where((x) => x.Id.Equals(id)).FirstOrDefault();
        }
    }
}
