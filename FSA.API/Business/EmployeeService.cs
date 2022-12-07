﻿using FSA.API.Business.Interfaces;
using FSA.API.Business.Model;
using FSA.API.Business.Services;
using FSA.API.Models;
using FSA.API.Models.Interface;
using FSA.Data.Repository;
using FSA.Data.Repository.GenericRepository;
using FSA.Domain.Entities;


namespace FSA.API.Business
{
    public class EmployeeService : IEmployeeService
    {
        private IRepository<Employee> _employeeRepository;



        public EmployeeService(IRepository<Employee> employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }


        public IAddEmployeeResult Add(AddEmployeeModel employee)
        {
            var employeeEntity = MapToEmployee(employee);
            if (HasDuplicateRecord(employeeEntity))
                return new AddEmployeeResult
                {
                    IsSuccess = false,
                    Message = "Employee already exists."
                };

            var result = _employeeRepository.Add(employeeEntity);

            return new AddEmployeeResult { IsSuccess = result.IsSuccess, Message = result.Error != null ? "There was a problem processing your request." : "" };
        }


        public IEnumerable<IEmployee> GetEmployees()
        {
            var dbEmployees = _employeeRepository.GetList().ToList();
            var employees = new List<IEmployee>();
            dbEmployees.ForEach(e => employees.Add(new EmployeeModel { ID = e.ID, Name = e.FirstName + " " + e.LastName }));
            return employees;
        }

        private Boolean HasDuplicateRecord(Employee employee)
        {
             return _employeeRepository.Get(e => e.FirstName == employee.FirstName && e.LastName == employee.LastName && e.MiddleName == employee.MiddleName) != null;
        }

        private Employee MapToEmployee(AddEmployeeModel employee)
        {
            return new Employee { FirstName = employee.FirstName, MiddleName = employee.MiddleName, LastName = employee.LastName };
        }
    }
}