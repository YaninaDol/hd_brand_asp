using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class SeasonRepository : GenericRepository<Season>, ISeasonRep
    {
        public SeasonRepository(HdBrandDboContext context) : base(context)

        {

        }

     

    }
}
