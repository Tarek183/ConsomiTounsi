using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ConsomiTounsi.web.Models
{
    public class EmployeeModel
    {
        [Key]
        public int EmployeId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string email { get; set; }
        public string phoneNumber { get; set; }
    }
}