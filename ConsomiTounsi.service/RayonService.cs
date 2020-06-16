using ConsomiTounsi.data.Infrastructure;
using ConsomiTounsi.domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.Service
{
    public class RayonService : Service<Rayon>, IRayonService {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public RayonService() : base(utk)
        {

        }

        public string GetrayoneById(int idrayon)
        {
            return GetById(idrayon).typeR.ToString();
        }
    }
}
