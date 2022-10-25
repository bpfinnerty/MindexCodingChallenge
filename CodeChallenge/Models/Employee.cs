using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{
    public class Employee
    {
        public String EmployeeId { get; set; }
        public String FirstName { get; set; }
        public String LastName { get; set; }
        public String Position { get; set; }
        public String Department { get; set; }
        public List<Employee> DirectReports { get; set; }

        /// <summary>
        /// This internal method of the employee class is used to calculate the total nu,mber of
        /// reports an employee has. This includes both its direct reports and all direct reports
        /// of the employees below it. 
        /// 
        /// It will recurse down the tree structure until all employees are accounted for.
        /// 
        /// The return value is an integer of the number of reports.
        /// </summary>
        /// <returns>int: Number of reports in an employee's DirectReports tree.</returns>
        public int countDirectReports (){
            
            int count = 0;
            // for each employee, we will recurse down their branch
            foreach(Employee report in this.DirectReports){
                count += (1 + report.countDirectReports());
            }
            return count;
        }
    }
}
