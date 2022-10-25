using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using CodeChallenge.Data;

namespace CodeChallenge.Repositories
{
    public class EmployeeRespository : IEmployeeRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<IEmployeeRepository> _logger;

        public EmployeeRespository(ILogger<IEmployeeRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Employee Add(Employee employee)
        {
            employee.EmployeeId = Guid.NewGuid().ToString();
            _employeeContext.Employees.Add(employee);
            return employee;
        }

        public Employee GetById(string id)
        {
            // EF core does not allow lazy loading by default. To fix this we eagerly load the first level
            // of the directReports hierarchy. We do not do this for all levels as we may not use that information,
            // or load in the entire database immediately.
            var employeeRetrieval = _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            return employeeRetrieval;
        }
        
        /// <summary>
        /// This method will build the entire hierarchy of the employee tree. This is done here incase our system does want 
        /// all subreports of our direct reports. Now we are able to utilize the deeply nested data but can 
        /// select when we want to use it. 
        /// 
        /// Returns an employee with the entire deep hierarchy of reports constructed
        /// </summary>
        /// <param name="id"> Employee ID to request from the database</param>
        /// <returns>Employee with fully filled out directReports</returns>
        public Employee GetRecursiveEmployee(String id)
        {
            // First we request the employee from the database
            var employeeRetrieval = _employeeContext.Employees.Include(e => e.DirectReports).SingleOrDefault(e => e.EmployeeId == id);
            // If that employee doesn't exist then return null
            if(employeeRetrieval == null)
                return null;
            
            // If that employee has no direct reports then we hit a leaf node
            if(employeeRetrieval.DirectReports.Count == 0)
                return employeeRetrieval;
            // Otherwise construct a list of direct reports and recurse on them.
            // Continue doing this until all employees under them are found.
            List<Employee> tempE = new List<Employee>();
            foreach(Employee direct_employee in employeeRetrieval.DirectReports){
                tempE.Add(GetRecursiveEmployee(direct_employee.EmployeeId));
            }
            // Finally set the substructure as the Direct Reports to give the employee the entire tree. Then return
            employeeRetrieval.DirectReports = tempE;
            return employeeRetrieval;
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }

        public Employee Remove(Employee employee)
        {
            return _employeeContext.Remove(employee).Entity;
        }
    }
}
