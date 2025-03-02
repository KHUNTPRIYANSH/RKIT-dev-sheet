﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using CRUD_Web_Api.Models;

namespace CRUD_Web_Api.Controllers
{
    /// <summary>
    /// It is a controller class which is used to perform CRUD operations on employee entity.
    /// </summary>
    public class EmployeesController : ApiController
    {

        // creating the list of employees

        /// <summary>
        /// The employee list [static]
        /// </summary>
        public static List<Employee> EmployeeList = new List<Employee>
        {
            // adding some dummy employee into the list
            new Employee{ID = 1,Name = "Mr. Abc ",City = "Goa",IsActive = true},
            new Employee{ID = 2,Name = "Mr. Xyz ",City = "Diu",IsActive = true},
            new Employee{ID = 3,Name = "Mis. Pqr",City = "JND",IsActive = false},
            new Employee{ID = 4,Name = "Mis. Abc",City = "RJK",IsActive = true},
        };

        // http verbs



        /// <summary>
        /// GET : api/Employees
        /// get method to display above list of data
        /// </summary>
        /// <returns>List of employees</returns>
        public List<Employee> Get()
        {
            return EmployeeList;
        }


        /// <summary>
        ///  GET : api/Employees/3
        ///  get method to display data data of any employee by id
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Single instance of employee</returns>
        public Employee Get(int id)
        {
            // lambda expression that finds specific employee by id from list and return obj of employee
            return EmployeeList.FirstOrDefault(e => e.ID == id);
        }


        /// <summary>
        /// POST : api/Employees
        /// post method to set data into the employees list 
        /// </summary>
        /// <param name="employee">The employee.</param>
        /// <returns>Updated List</returns>
        public List<Employee> Post(Employee employee)
        {
            // here we can do some final validation before adding data to table / db
            EmployeeList.Add(employee);

            // returning updated list of data (optional)
            return EmployeeList;
        }

        /// <summary>
        /// PUT : api/Employees/3
        /// put method to update data of any employee by id into the employees list        
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="employee">The employee.</param>
        /// <returns>Updated List</returns>
        public List<Employee> Put(int id, Employee employee)
        {
            // following list find specific employee form the list if found return the obj else null
            var emp = EmployeeList.FirstOrDefault(e => e.ID == id);

            // if found
            if (emp != null)
            {
                // update the info of that employee 
                emp.Name = employee.Name;
                emp.City = employee.City;
                emp.IsActive = emp.IsActive;
            }

            // returning updated list of data 
            return EmployeeList;
        }

        /// <summary>
        ///  DELETE : api/Employees/3
        ///  delete method to delete data of any employee by id into the employees list        
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Updated List</returns>
        public List<Employee> Delete(int id)
        {
            // following list find specific employee form the list if found return the obj else null
            var emp = EmployeeList.FirstOrDefault(e => e.ID == id);

            // if found
            if (emp != null)
            {
                // remove employee form employee list 
                EmployeeList.Remove(emp);
            }

            // returning updated list of data 
            return EmployeeList;
        }

    }
}
