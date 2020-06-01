using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.data.Infrastructure
{
   public interface IUnitOfWork:IDisposable
    {
        void commit();
        IRepositoryBase<T> GetRepositoryBase<T>() where T:class;
        



        
    }
}
