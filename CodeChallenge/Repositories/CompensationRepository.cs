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
    public class CompensationRepository : ICompensationRepository
    {
        // These private variable allow the compensation repository to 
        // access the in memory database and log our actions.
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        /// <summary>
        /// Constructor for Compensation repository. Given an EmployeeContest and a logger.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="employeeContext"></param>
        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _logger = logger;
            _employeeContext = employeeContext;
        }

        /// <summary>
        /// The add method will put any valid compensation object into our database table. 
        /// Each CompensationId must be unique which means that each employee is associated with
        /// exactly 1 compensation object.
        /// </summary>
        /// <param name="compensation"> Compensation object to be added</param>
        /// <returns>The Compensation object added in the database</returns>
        public Compensation Add(Compensation compensation)
        {
            _employeeContext.Compensations.Add(compensation);
            return compensation;
        }

        /// <summary>
        /// This method retrieves a compensation object from the database given
        /// the employeeId associated with it. Here an employee id acts both
        /// as compensations primary and foreign key, thus ensuring quick lookup
        /// and uniqueness. The compensation object retrieved from the database is
        /// returned.
        /// </summary>
        /// <param name="id">Employee id related to the compensation object</param>
        /// <returns>The compensation object retrieved from the database</returns>
        public Compensation GetById(String id)
        {
            return _employeeContext.Compensations.SingleOrDefault(c => c.CompensationId == id);
        }

        public Task SaveAsync()
        {
            return _employeeContext.SaveChangesAsync();
        }
    }
}