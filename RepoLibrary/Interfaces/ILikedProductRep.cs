using RepoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoriesLibrary.Models;
using hd_brand_asp.Models;

namespace RepoLibrary
{
    public interface ILikedProductRep : IGenericRepository<LikedProduct>
    {
       
        IEnumerable<Product> GetAllProducts(string userId);
        bool setProduct(string userId, int prodId, bool like);
        bool getProduct(int prodId);

    }
}
