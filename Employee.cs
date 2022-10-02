using System;
using System.Collections.Generic;
using System.Text;
using Lahiye.Interfaces;
using Lahiye.Services;

namespace Lahiye.Models
{
    class Employee
    {
        static int _no;
        public string No;
        public string Fullname;
        private string _position;
        public string Position
        {
            get => _position;
            set
            {
                while (value.Length < 2)
                {
                    Console.WriteLine("Duzgun vezife daxil edin.Iscinin vezifesi minimum 2 herfden ibaret olmalidir:");
                    value = Console.ReadLine();
                }
                _position = value;
            }
        }
        private double _salary;
        public double Salary
        {
            get => _salary;
            set
            {
                while (value < 250)
                {
                    Console.WriteLine("Duzgun SalaryLimit daxil edin. Minimum 250 ola biler:");
                    double.TryParse(Console.ReadLine(), out value);
                }
                _salary = value;
            }
        }
        public string DepartmentName;
        static Employee()
        {
            _no = 1000;
        }
        public Employee(string fullname, string position, string departmentName, double salary)
        {
            Fullname = fullname;
            Position = position;
            DepartmentName = departmentName;
            Salary = salary;
            _no++;
            No = $"{departmentName[0]}{departmentName[1]}{_no}";
        }
        public override string ToString()
        {
            return $"{Fullname} {Position} {DepartmentName} {No}";
        }
    }
}
