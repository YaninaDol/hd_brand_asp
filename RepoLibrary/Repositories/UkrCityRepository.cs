using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class UkrCityRepository : GenericRepository<UkrCity>, IUkrCityRep
    {
        public UkrCityRepository(HdBrandDboContext context) : base(context)

        {

        }

     
    }
}
