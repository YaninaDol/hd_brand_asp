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
    public interface ICategoryRep:IGenericRepository<Category>
    {
        bool DeleteCategory(int Id);
        bool UpdateCategory(int id,string categoryName);


    }
}
