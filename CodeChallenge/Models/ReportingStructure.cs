namespace CodeChallenge.Models
{
    public class ReportingStructure
    {
        // As Reporting Structure is not persisted it will not have an id. 
        // Instead the full employee and number of reports will be recalculated
        // for each get request.
        public Employee Employee { get; set; }
        public int NumberOfReports { get; set; }

    }
}