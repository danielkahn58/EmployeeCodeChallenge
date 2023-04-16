using CodeChallenge.Data;
using CodeChallenge.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace CodeChallenge.Repositories
{
    public class CompensationRepository : ICompensationRepository
    {
        private readonly EmployeeContext _employeeContext;
        private readonly ILogger<ICompensationRepository> _logger;

        public CompensationRepository(ILogger<ICompensationRepository> logger, EmployeeContext employeeContext)
        {
            _employeeContext = employeeContext;
            _logger = logger;
        }

        public Compensation GetByEmployeeId(String id)
        {
            return _employeeContext.Compensations.Include(e => e.Employee).SingleOrDefault(c => c.EmployeeId == id);
        }
        public Compensation Add(Compensation comp)
        {
            _employeeContext.Compensations.Add(comp);
            _employeeContext.SaveChanges();
            return comp;
        }
    }
}