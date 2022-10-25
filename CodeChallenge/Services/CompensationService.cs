using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class CompensationService : ICompensationService
    {
        private readonly ICompensationRepository _compensationRepository;

        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<CompensationService> _logger;

        public CompensationService(ILogger<CompensationService> logger, ICompensationRepository compensationRepository, IEmployeeRepository employeeRepository)
        {
            _compensationRepository = compensationRepository;
            _logger = logger;
            _employeeRepository = employeeRepository;
        }

        /// <summary>
        /// This method will provide controllers the ability to add compensation entities to the database.
        /// Compensation will only be added if the employee id is valid i.e. only if that employee exists
        /// in the database. There it is dangerous to assign non-existent employees to have a salary.
        /// 
        /// It will return the compensation object if valid or null otherwise
        /// </summary>
        /// <param name="compensation">Compensation object to be added to the database</param>
        /// <returns>Compensation entity if successfull, null otherwise</returns>
        public Compensation Create(Compensation compensation)
        {
            if(compensation != null)
            {
                // If an employee doesn't exist then we shouldn't add any compensation for them
                Employee employeeExists = _employeeRepository.GetById(compensation.CompensationId);
                if(employeeExists != null)
                {
                    _compensationRepository.Add(compensation);
                    _compensationRepository.SaveAsync().Wait();
                    return compensation;
                }
                return null;
            }
            return null;
        }

        /// <summary>
        /// This method will take an employee id and retrieve a compensation entity
        /// from the database. If the employee id is null or empty then null
        /// will be returned instead.
        /// </summary>
        /// <param name="id">String employee id to query</param>
        /// <returns>Compensation object with matching id</returns>
        public Compensation GetById(String id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _compensationRepository.GetById(id);
            }
            return null;
        }
    }
}