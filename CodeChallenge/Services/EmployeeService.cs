using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Microsoft.Extensions.Logging;
using CodeChallenge.Repositories;

namespace CodeChallenge.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly ILogger<EmployeeService> _logger;

        public EmployeeService(ILogger<EmployeeService> logger, IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            _logger = logger;
        }

        public Employee Create(Employee employee)
        {
            if(employee != null)
            {
                _employeeRepository.Add(employee);
                _employeeRepository.SaveAsync().Wait();
            }

            return employee;
        }

        public Employee GetById(string id)
        {
            if(!String.IsNullOrEmpty(id))
            {
                return _employeeRepository.GetById(id);
            }

            return null;
        }

        public Employee Replace(Employee originalEmployee, Employee newEmployee)
        {
            if(originalEmployee != null)
            {
                _employeeRepository.Remove(originalEmployee);
                if (newEmployee != null)
                {
                    // ensure the original has been removed, otherwise EF will complain another entity w/ same id already exists
                    _employeeRepository.SaveAsync().Wait();

                    _employeeRepository.Add(newEmployee);
                    // overwrite the new id with previous employee id
                    newEmployee.EmployeeId = originalEmployee.EmployeeId;
                }
                _employeeRepository.SaveAsync().Wait();
            }

            return newEmployee;
        }

        public ReportingStructure GetReportingStructure(String employeeId)
        {
            ReportingStructure rs = new ReportingStructure();
            rs.Employee = GetById(employeeId);
            rs.NumberOfReports = this.GetTotalReports(rs.Employee);
            return rs;
        }

        public int GetTotalReports(Employee employee)
        {
            int totalReports = 0;
            List<Employee> directReports = _employeeRepository.GetById(employee.EmployeeId).DirectReports; // Note : an inefficient 

            if (directReports != null)
            {
                totalReports += directReports.Count;
                foreach (Employee e in employee.DirectReports)
                {
                    totalReports += this.GetTotalReports(e);
                }
                return totalReports;
            }
            else // If there are no direct reports
                return 0;

        }
    }
}
