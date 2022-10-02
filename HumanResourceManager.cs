using System;
using System.Collections.Generic;
using System.Text;
using Lahiye.Interfaces;
using Lahiye.Models;



namespace Lahiye.Services
{
    class HumanResourceManager : IHumanResourceManager
    {
        Department[] _department;

        public HumanResourceManager()
        {
            _department = new Department[5];
            Department FINANCE = new Department("FINANCE", 20, 8000);
            Department SALES = new Department("SALES", 25, 15000);
            Department MARKETING = new Department("MARKETING", 30, 30000);
            Department MANAGEMENT = new Department("MANAGEMENT", 10, 85000);
            Department TECHNOLOGY = new Department("TECHNOLOGY", 15, 35000);
            _department[0] = FINANCE;
            _department[1] = SALES;
            _department[2] = MARKETING;
            _department[3] = MANAGEMENT;
            _department[4] = TECHNOLOGY;
        }

        public Department[] Departments => _department;
        public void AddDepartment(string name, int workerLimit, double salaryLimit)
        {
            Department department = new Department(name.ToUpper(), workerLimit, salaryLimit);
            Array.Resize(ref _department, _department.Length + 1);
            _department[_department.Length - 1] = department;
        }
        public void EditDepartments(ref string oldDepartmentName, ref string newDepartmentName)
        {
            foreach (Department department in _department)
            {
                if (department.Name == oldDepartmentName.ToUpper())
                {
                    department.Name = newDepartmentName.ToUpper();
                }
            }
        }
        public void AddEmployee(string departmentName, string fullname, string position, double salary)
        {
            foreach (Department department in _department)
            {
                if (departmentName == department.Name)
                {                  
                        if (department.WorkerLimit > department.Employees.Length && salary <= (department.SalaryLimit - (department.CalcSalaryAverage() * department.Employees.Length)))
                        {
                            Employee _employee = new Employee(fullname, position, departmentName, salary);
                            Array.Resize(ref department.Employees, department.Employees.Length + 1);
                            department.Employees[department.Employees.Length - 1] = _employee;
                        }
                
                }

            }
        }
        public void EditEmployee(string departmentName, string no, string fullname, double newSalary, string newPosition)
        {
            foreach (Department department in _department)
            {
                if (departmentName == department.Name)
                {
                    if (department.Employees.Length > 0)
                    {
                        foreach (Employee employee in department.Employees)
                        {
                            if (employee.No == no)
                            {
                                if (department.SalaryLimit >= newSalary + (department.CalcSalaryAverage() * department.Employees.Length))
                                {
                                    employee.Salary = newSalary;
                                    employee.Position = newPosition;
                                }
                            }

                        }

                    }
                    else
                    {
                        Console.WriteLine("Departmentde isci yoxdur. Once isci elave edin:");
                    }
                    
                }
            }
        }
        public Department[] GetDepartments()
        {
            return _department;

        }
        public bool CheckDepartmentsByName(string departmentName)
        {
            foreach (Department department in _department)
                if ((department.Name == departmentName.ToUpper()))
                    return true;

            return false;
        }
        public bool CheckEmployeesByNo(string no)
        {
            foreach (Department department in _department)
            {
                foreach (Employee employee in department.Employees)
                {
                    if (employee.No == no.ToUpper())
                    {
                        return true;

                    }
                }
            }
            return false;
        }

        public void RemoveEmployee(string departmentName, string employeeNo)
        {
            foreach (Department department in _department)
            {
                if (departmentName == department.Name)
                {
                    foreach (Employee employee in department.Employees)
                    {
                        if (employeeNo.ToUpper() == employee.No)
                        {
                            Array.IndexOf(department.Employees, null);
                            Array.Resize(ref department.Employees, department.Employees.Length-1);

                        }
                    }
                }
            }
        }

        
    } 
}
