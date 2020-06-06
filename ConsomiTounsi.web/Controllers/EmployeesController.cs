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
                    EmployeId = e.EmployeId,
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
            return Json(result);
        }

        // GET: api/Employees/5
        public IHttpActionResult Get(int id)
        {
            Employee e = ES.GetById(id);
            Employee ndEmpl = new Employee
            {
                EmployeId = e.EmployeId,
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
                var existingFormation = ctx.Employees.Where(e => e.EmployeId == em.EmployeId)
                                                        .FirstOrDefault<Employee>();

                if (existingFormation != null)
                {
                    existingFormation.EmployeId = em.EmployeId;
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

        //public IHttpActionResult Put(int id, EmployeeModel student)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest("Not a valid model");
        //    using (var ctx = new MyContext())
        //    {
        //        var existingStudent = ctx.Events.Where(s => s.Id == id)
        //                                                .FirstOrDefault<Event>();
        //        if (existingStudent != null)
        //        {
        //            existingFormation.EmployeId = em.EmployeId;
        //            existingFormation.FirstName = em.FirstName;
        //            existingFormation.LastName = em.LastName;
        //            existingFormation.email = em.email;
        //            existingFormation.phoneNumber = em.phoneNumber;
        //            ctx.SaveChanges();
        //        }
        //        else
        //        {
        //            return NotFound();
        //        }
        //    }
        //    return Ok();
        //}


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