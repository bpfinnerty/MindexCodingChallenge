using CodeChallenge.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CodeChallenge.Services
{
    public interface ICompensationService
    {
        Compensation GetById(String id);
        Compensation Create(Compensation compensation);
    }
}