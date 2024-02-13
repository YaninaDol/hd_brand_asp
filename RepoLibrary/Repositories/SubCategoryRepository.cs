﻿using RepoLibrary.Interfaces;
using hd_brand_asp.Data;
using hd_brand_asp.Models;

namespace RepoLibrary.Repositories
{
    public class SubCategoryRepository : GenericRepository<SubCategory>, ISubCategoryRep
    {
        public SubCategoryRepository(HdBrandDboContext context) : base(context)

        {

        }

     
    }
}
