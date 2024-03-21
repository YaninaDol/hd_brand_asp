using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class ContentVideoRepository : GenericRepository<ContentVideo>, IContentVideo
    {
        public ContentVideoRepository(HdBrandDboContext context) : base(context)

        {

        }

     
    }
}
