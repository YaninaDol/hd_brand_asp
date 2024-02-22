using RepositoriesLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using hd_brand_asp.Data;
using RepoLibrary.Interfaces;
using RepoLibrary.Repositories;

namespace RepoLibrary.UnitofWork
{
    public class  UnitOfWork : IUnitOfWork
    {
        private readonly HdBrandDboContext _context;
     
        public UnitOfWork(HdBrandDboContext context)
        {
            _context = context;
            CategoryRep = new CategoryRepository(_context);

            ProductRep = new ProductRepository(_context);
            SubCategoryRep = new SubCategoryRepository(_context); 
            MaterialRep = new MaterialRepository(_context);
            SizeRep = new SizeRepository(_context); 
            ProductssizeRep = new ProductssizeRepository(_context);

        }
    


        public ICategoryRep CategoryRep { get;  }

        public IProductRepository ProductRep { get; }
        public ISubCategoryRep SubCategoryRep { get; }
        public IMaterialRep MaterialRep { get; }
        public ISizesRep SizeRep { get; }
        public IProductssize ProductssizeRep { get; }

        public int Commit()=>_context.SaveChanges();    

        public void Dispose()=>_context?.Dispose(); 
    }
}
