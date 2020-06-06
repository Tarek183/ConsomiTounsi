using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsomiTounsi.domain.Entities
{
    class Employee
    {
        [Key]
        public int EmployeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }


    }
}
