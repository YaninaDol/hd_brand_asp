using RepoLibrary.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoriesLibrary.Models;
using hd_brand_asp.Models;
using Microsoft.EntityFrameworkCore;

namespace RepoLibrary
{
    public interface ICategoryRep:IGenericRepository<Category>
    {

        public List<SubCategory> GetSubCategoryNamesByCategoryId(int categoryId);

        public List<Material> MaterialNamesByCategoryId(int categoryId);

    }
}
