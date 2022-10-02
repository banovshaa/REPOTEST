using System;
using System.Collections.Generic;
using System.Text;
using Lahiye.Services;
using Lahiye.Interfaces;


namespace Lahiye.Models

{
    class Department
    {
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                while (value.Length < 2)
                {
                    Console.WriteLine("Duzgun Department adi daxil edin. Minimum 2 herfden ibaret olmalidir");
                    value = Console.ReadLine();
                }
                _name = value;

            }
        }
        private int _workerLimit;
        public int WorkerLimit
        {
            get => _workerLimit;
            set
            {

                while (value < 1)
                {
                    Console.WriteLine("Duzgun WorkerLimit daxil edin. Minimum 1 ola biler:");
                    int.TryParse(Console.ReadLine(), out value);
                }
                _workerLimit = value;
            }

        }
        private double _salaryLimit;
        public double SalaryLimit
        {
            get => _salaryLimit;
            set
            {
                double salaryLimitNum;
                while (value < 250 )
                {
                    Console.WriteLine("Duzgun SalaryLimit daxil edin. Her isci ucun minimum salary deyerini nezere alin:");
                    double.TryParse(Console.ReadLine(), out salaryLimitNum);
                }
                _salaryLimit = value;

            }
        }
        public Employee[] Employees;
        public Department(string name, int workerLimit, double salaryLimit)
        {
            Employees = new Employee[0];
            Name = name;
            WorkerLimit = workerLimit;
            SalaryLimit = salaryLimit;
        }
        public override string ToString()
        {
            return $"{_name} {_workerLimit} {_salaryLimit}";

        }
        public double CalcSalaryAverage()
        {
            double totalSalary = 0;           
            foreach (Employee employee in Employees)
            {
                totalSalary += employee.Salary;
               
            }
            return totalSalary / Employees.Length;
        }

    }
}

