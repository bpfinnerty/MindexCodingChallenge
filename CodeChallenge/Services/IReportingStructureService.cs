using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge.Services
{
    public interface IReportingStructureService
    {
        ReportingStructure GetById(String id);
    }
}