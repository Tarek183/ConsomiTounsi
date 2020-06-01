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
    public class CommentService : Service<Comment>, ICommentService
    {
        static IDataBaseFactory Factory = new DataBaseFactory();
        static IUnitOfWork utk = new UnitOfWork(Factory);
        public CommentService() : base(utk)
        {

        }
    }
}
