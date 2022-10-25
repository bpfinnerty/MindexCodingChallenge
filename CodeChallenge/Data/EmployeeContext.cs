using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Data
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {

        }

        // These are the two tables in our database. Compensation became a table because it is
        // persisted in the database. ReportingStructure is not stored in any table because
        // it is calculated on the fly from our employee table.
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Compensation> Compensations { get; set; }
    }
}
