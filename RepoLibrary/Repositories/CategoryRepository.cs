﻿using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRep
    {
        public CategoryRepository(HdBrandDboContext context) : base(context)

        {

        }

     
    }
}
