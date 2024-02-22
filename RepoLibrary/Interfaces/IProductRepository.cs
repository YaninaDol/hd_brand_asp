using hd_brand_asp.Models;
using RepoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary
{
    public interface IProductRepository : IGenericRepository<Product>
    {
        IEnumerable<Productssize> getSizes(int idProduct);
        IEnumerable<Product> getByCategory(int id);
        IEnumerable<Product> getByShoeType(int id);
        IEnumerable<Product> getByMaterial(int id);
        IEnumerable<Product> getBySeason(int id);
        bool procedure(string id,Product item);
        IEnumerable<Productssize> updateprocedure(int id);
        bool deleteSize(int Id,int Size);

    }
}
