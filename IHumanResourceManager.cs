using System;
using System.Collections.Generic;
using System.Text;
using Lahiye.Models;
using Lahiye.Services;

namespace Lahiye.Interfaces
{
    interface IHumanResourceManager
    {

        Department[] Departments { get; }
        void AddDepartment(string name, int workerLimit, double salaryLimit);
        Department[] GetDepartments();
        void EditDepartments(ref string oldDepartmentName, ref string newDepartmentName);
        void AddEmployee(string departmentName, string fullname, string position, double salary);
        void EditEmployee(string departmentName, string no, string fullname, double salary, string position);
        void RemoveEmployee(string departmentName, string employeeNo);
        bool CheckDepartmentsByName(string name);
        bool CheckEmployeesByNo(string no);
      
        
        


    }
}
