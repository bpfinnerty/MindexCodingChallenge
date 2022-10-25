using System;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class ReportingStructureService : IReportingStructureService
    {   
        // Here I reference the employee repository because there is no data access required for
        // reporting structure. Since there is no table we simple have to get the employees 
        // from the employee table.
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public ReportingStructureService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        /// <summary>
        /// This method will take an employee id and return a reporting Structure entity.
        /// 
        /// It will dynamically construct the entire deep hierarchy of the direct reports tree
        /// for the employee and calculate the the number of reports.
        /// 
        /// What will be returned is a reportingStructure entity or null if the employee id
        /// is invalid, or if the employee does not exist.
        /// </summary>
        /// <param name="id">Employee id to query.</param>
        /// <returns>Reporting Structure entity</returns>
        public ReportingStructure GetById(String id){
            
            if(!String.IsNullOrEmpty(id)){
                // get the employee
                Employee employee = _employeeRepository.GetById(id);
                if(employee != null){
                    // Construct the entire deep hierarchy for an employee
                    Employee fullEmployee = _employeeRepository.GetRecursiveEmployee(employee.EmployeeId);
                    // create a reporting structure and use the employee function to calculate the
                    // number of reports
                    ReportingStructure reportingStructure = new ReportingStructure{
                        Employee = fullEmployee,
                        NumberOfReports = fullEmployee.countDirectReports()
                    };
                    return reportingStructure;
                }
            }
            return null;
        }
    }
}