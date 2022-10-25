using System;
using System.Linq;
using System.Threading.Tasks;

namespace CodeChallenge.Models
{

    public class Compensation
    {
    
       // Each compensation object will have an ID. This will be the related employee id 
       public String CompensationId { get; set; }

       // Salary will be an int but could also be a string if you wanted commas. 
       public int Salary { get; set; }

       // Effective date will be a string in the form YYYY-MM-DD
       public String EffectiveDate { get; set; }
    }
}