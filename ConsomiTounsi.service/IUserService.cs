﻿using ConsomiTounsi.domain.Entities;
using Service.Pattern;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.service
{
    public interface IUserService : IService<User>
    {
        IEnumerable<User> SearchUserByUserName(string searchString);
        List<User> getUsers();
    }
}
