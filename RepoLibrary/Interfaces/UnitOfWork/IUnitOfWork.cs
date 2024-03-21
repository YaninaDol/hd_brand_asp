using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        
        public IProductRepository ProductRep { get; }

        public ICategoryRep CategoryRep { get; }
        public ISubCategoryRep SubCategoryRep { get; }
        public IMaterialRep MaterialRep { get; }
        public ISizesRep SizeRep { get; }
        public IProductssize ProductssizeRep { get; }
        public IColorRep ColorRep { get; }
        public IUkrCityRep UkrCityRep { get; }
        public IUserInfo UserInfo { get; }
        public ILikedProductRep LikedProducts { get; }
        public IContentVideo ContentVideos { get; }
        public int Commit();
    }
}
