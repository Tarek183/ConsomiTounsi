using ConsomiTounsi.data.Infrastructure;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.Service;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.service {
    public class CouponsService : Service<Coupons>, ICouponsService {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public CouponsService() : base(utk)
        {

        }
    }
}
