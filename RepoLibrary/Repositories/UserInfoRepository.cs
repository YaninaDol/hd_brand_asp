using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;
using RepositoriesLibrary.Models;

namespace RepoLibrary.Repositories
{
    public class UserInfoRepository : GenericRepository<Userinfo>, IUserInfo
    {
        public UserInfoRepository(HdBrandDboContext context) : base(context)

        {

        }

        public Userinfo GetUserinfo(string id)
        {
          return db.Userinfos.Where((x)=>x.UserId.Equals(id)).FirstOrDefault();
        }
    }
}
