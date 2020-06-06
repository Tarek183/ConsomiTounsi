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
        public void Put(int id, [FromBody]string value)
        {
        }

        // PUT: api/Employees/5
        [HttpPut]
        public IHttpActionResult Put(EmployeeModel em)
        {
            if (!ModelState.IsValid)
                return BadRequest("Not a valid model");

            using (var ctx = new MyContext())
            {
                var existingFormation = ctx.Employees.Where(p => p.EmployeId == em.EmployeId)
                                                        .FirstOrDefault<Employee>();

                if (existingFormation != null)
                {
                    //existingFormation.EmployeId = em.EmployeId;
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

        // PUT: api/Employees/5
        //[ResponseType(typeof(void))]
        //public IHttpActionResult PutEmployee(int id, Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    if (id != employee.EmployeId)
        //    {
        //        return BadRequest();
        //    }

        //    db.Entry(employee).State = EntityState.Modified;

        //    try
        //    {
        //        db.SaveChanges();
        //    }
        //    catch (DbUpdateConcurrencyException)
        //    {
        //        if (!EmployeeExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }

        //    return StatusCode(HttpStatusCode.NoContent);
        //}

        // POST: api/Employees
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult PostEmployee(Employee employee)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return BadRequest(ModelState);
        //    }

        //    db.Employees.Add(employee);
        //    db.SaveChanges();

        //    return CreatedAtRoute("DefaultApi", new { id = employee.EmployeId }, employee);
        //}

        //// DELETE: api/Employees/5
        //[ResponseType(typeof(Employee))]
        //public IHttpActionResult DeleteEmployee(int id)
        //{
        //    Employee employee = db.Employees.Find(id);
        //    if (employee == null)
        //    {
        //        return NotFound();
        //    }

        //    db.Employees.Remove(employee);
        //    db.SaveChanges();

        //    return Ok(employee);
        //}

        //protected override void Dispose(bool disposing)
        //{
        //    if (disposing)
        //    {
        //        db.Dispose();
        //    }
        //    base.Dispose(disposing);
        //}

        //private bool EmployeeExists(int id)
        //{
        //    return db.Employees.Count(e => e.EmployeId == id) > 0;
        //}
    }
}