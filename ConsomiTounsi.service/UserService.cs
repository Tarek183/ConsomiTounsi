using ConsomiTounsi.data.Infrastructure;
using ConsomiTounsi.domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.service
{
    public class UserService : Service<User>, IUserService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public UserService() : base(utk)
        {

        }
        public List<User> getUsers()
        {
            List<User> list = new List<User>();
            return list;
        }

        public IEnumerable<User> SearchUserByUserName(string searchString)
        {
            IEnumerable<User> Users = GetMany();
            if (!String.IsNullOrEmpty(searchString))
            {
                //Users = GetMany(x => x.Username.Contains(searchString));
            }

            return Users;
        }
    }
}
