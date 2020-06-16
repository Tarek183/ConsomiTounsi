using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using ConsomiTounsi.data;
using ConsomiTounsi.domain.Entities;
using ConsomiTounsi.service;
using ConsomiTounsi.web.Models;

namespace ConsomiTounsi.web.Controllers
{
    public class EmployeesController : ApiController
    {
        IEmployeeService ES;
        List<EmployeeModel> result = new List<EmployeeModel>();
        public EmployeesController()
        {
            ES = new EmployeeService();
            Index();
            result = Index().ToList();
        }
        public List<EmployeeModel> Index()
        {
            IEnumerable<Employee> Employees = ES.GetMany();
            List<EmployeeModel> EmployeesXml = new List<EmployeeModel>();
            foreach (Employee e in Employees)
            {
                EmployeesXml.Add(new EmployeeModel
                {
                    employeId = e.employeId,
                    FirstName = e.FirstName,
                    LastName = e.LastName,
                    email = e.email,
                    phoneNumber = e.phoneNumber
                });
            }
            return EmployeesXml;
        }
        // GET: api/Employees
        [HttpGet]
        public IHttpActionResult Get()
        {
            List<Employee> emps = new List<Employee>();
            using (var ctx = new MyContext())
            {
                foreach (var emp in ctx.Employees.ToList())
                {
                    Employee ndEmpl = new Employee
                    {
                        employeId = emp.employeId,
                        FirstName = emp.FirstName,
                        LastName = emp.LastName,
                        email = emp.email,
                        phoneNumber = emp.phoneNumber
                    };
                    emps.Add(ndEmpl);
                }
                return Json(emps);
            }
        }

        // GET: api/Employees/5
        public IHttpActionResult Get(int id)
        {
            Employee e = ES.GetById(id);
            Employee ndEmpl = new Employee
            {
                employeId = e.employeId,
                FirstName = e.FirstName,
                LastName = e.LastName,
                email = e.email,
                phoneNumber = e.phoneNumber
            };
            return Json(ndEmpl);
        }
        // Post: api/Employees
        [HttpPost]
        public IHttpActionResult Post(Employee emp)
        {
            using (var ctx = new MyContext())
            {
                ctx.Employees.Add(emp);
                ctx.SaveChanges();
            }
            return Ok();
        }

        // PUT: api/Employees/5
        [HttpPut]
        public IHttpActionResult Put(int id, EmployeeModel em)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new MyContext())
            {
                var existingFormation = ctx.Employees.Where(e => e.employeId == id)
                                                        .FirstOrDefault<Employee>();

                if (existingFormation != null)
                {
                    existingFormation.FirstName = em.FirstName;
                    existingFormation.LastName = em.LastName;
                    existingFormation.email = em.email;
                    existingFormation.phoneNumber = em.phoneNumber;
                    ctx.SaveChanges();
                }
                else
                {
                    return NotFound();
                }
            }
            return Ok();
        }

        // DELETE: api/Employees/5
        public IHttpActionResult Delete(int id)
        {
            Employee emp = ES.GetById(id);
            ES.Delete(emp);
            ES.Commit();
            return Ok(emp);
        }
    }
}