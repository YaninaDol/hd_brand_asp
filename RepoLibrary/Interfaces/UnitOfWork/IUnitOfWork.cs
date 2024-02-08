using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepoLibrary.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        public ICategoryRep CategoryRep { get; }
      
        public IProductRepository ProductRep { get; }
       public int Commit();
    }
}
