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

        }
    


        public ICategoryRep CategoryRep { get;  }

        public IProductRepository ProductRep { get; }

        public int Commit()=>_context.SaveChanges();    

        public void Dispose()=>_context?.Dispose(); 
    }
}
