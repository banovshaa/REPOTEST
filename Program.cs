using System;
using System.Text;
using System.Linq;
using Lahiye.Models;
using Lahiye.Services;
using Lahiye.Interfaces;

namespace Lahiye
{
    class Program
    {
        static void Main(string[] args)
        {
            IHumanResourceManager humanResourceManager = new HumanResourceManager();
            do
            {
                Console.WriteLine("Etmek istediyiniz emeliyyata uygun reqemi secin:");
                Console.WriteLine("1. Departamentlerle bagli");
                Console.WriteLine("2. Iscilerle bagli");
                string answerStr1 = Console.ReadLine();
                int answerNum1;
                while (!int.TryParse(answerStr1, out answerNum1) || answerNum1 < 1 || answerNum1 > 2)
                {
                    Console.WriteLine("Duzgun secim daxil edilmeyib. Yeniden sinayin:");
                    answerStr1 = Console.ReadLine();
                }
                switch (answerNum1)
                {
                    case 1:
                        do
                        {
                            Console.WriteLine("Departamentlerle bagli etmek istediyiniz emeliyyata uygun reqemi secin:");
                            Console.WriteLine("1. Departamentlerin siyahisini gormek");
                            Console.WriteLine("2. Departament yaratmaq");
                            Console.WriteLine("3. Departamentde deyisiklik etmek");
                            string answerStr2 = Console.ReadLine();
                            int answerNum2;
                            while (!int.TryParse(answerStr2, out answerNum2) || answerNum2 < 1 || answerNum2 > 3)
                            {
                                Console.WriteLine("Duzgun secim daxil edilmeyib. Yeniden sinayin:");
                                answerStr2 = Console.ReadLine();
                            }
                            switch (answerNum2)
                            {
                                case 1:
                                    Departments(humanResourceManager);
                                    break;
                                case 2:
                                    AddDepartment(ref humanResourceManager);
                                    break;
                                case 3:
                                    EditDepartment(ref humanResourceManager);
                                    break;
                            }
                        }
                        while (true);
                        break;
                    case 2:
                        do
                        {
                            Console.WriteLine("Iscilerle bagli etmek istediyiniz emeliyyata uygun reqemi secin:");
                            Console.WriteLine("1. Butun iscilerin siyahisini gormek");
                            Console.WriteLine("2. Departamentdeki iscilerin siyahisini gormek");
                            Console.WriteLine("3. Isci elave etmek");
                            Console.WriteLine("4. Isci uzerinde deyisiklik etmek");
                            Console.WriteLine("5. Departamentden isci silmek");
                            string answerStr3 = Console.ReadLine();
                            int answerNum3;
                            while (!int.TryParse(answerStr3, out answerNum3) || answerNum3 < 1 || answerNum3 > 5)
                            {
                                Console.WriteLine("Duzgun secim daxil edilmeyib. Yeniden sinayin:");
                                answerStr3 = Console.ReadLine();
                            }
                            switch (answerNum3)
                            {
                                case 1:
                                    ShowAllEmployees(humanResourceManager);
                                    break;
                                case 2:
                                    ShowEmployeesByDepartments(humanResourceManager);                                    
                                    break;
                                case 3:
                                    AddEmployee(ref humanResourceManager);
                                    break;
                                case 4:
                                    EditEmployee(ref humanResourceManager);
                                    break;
                                case 5:
                                    RemoveEmployee(ref humanResourceManager);
                                    break;
                            }

                        } while (true);

                        break;
                }

            } while (true);
        }
        static void Departments(IHumanResourceManager humanResourceManager)
        {
            foreach (Department department in humanResourceManager.Departments)
            {
                double averageSalaryResult = 0;
                if (department.Employees.Length < 1)
                {
                    averageSalaryResult = 0;
                }
                else
                {
                    averageSalaryResult = department.CalcSalaryAverage();
                }
                Console.WriteLine($"{department} {averageSalaryResult}");
            }
        }
        static void AddDepartment(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Elave etmek istediyiniz Departamentin adini daxil edin:");
            string name = Console.ReadLine();
            name = name.ToUpper();
            foreach (Department department in humanResourceManager.Departments)
            {
                if (department.Name == name.ToUpper())
                {
                    Console.WriteLine("Daxil edilen adda departament movcuddur. Basqa ad daxil edin: ");
                    name = Console.ReadLine();
                }
                while (name.Length < 2)
                {
                    Console.WriteLine("Duzgun ad daxil edin. Ad minimum 2 herfden ibaret olmalidir: ");
                    name = Console.ReadLine();
                }
            }
            Console.WriteLine("Isci Limiti daxil edin:");
            string workerLimitStr = Console.ReadLine();
            int workerLimitNum;
            while (!int.TryParse(workerLimitStr, out workerLimitNum) || workerLimitNum < 1)
            {
                Console.WriteLine("Duzgun limit deyeri daxil edin. Isci limiti minimum 1 ola biler:");
                workerLimitStr = Console.ReadLine();
            }

            Console.WriteLine("Salary Limit daxil edin:");
            string salaryLimitStr = Console.ReadLine();
            double salaryLimitNum;

            while (!double.TryParse(salaryLimitStr, out salaryLimitNum) || salaryLimitNum < 250 * workerLimitNum)
            {
                Console.WriteLine($"Duzgun SalaryLimit daxil edin. Minimum salary {workerLimitNum * 250} olmalidir:");
                salaryLimitStr = Console.ReadLine();
            }
            humanResourceManager.AddDepartment(name, workerLimitNum, salaryLimitNum);
        }
        static void EditDepartment(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Duzelis etmek istediyiniz departamentin adini daxil edin");
            Departments(humanResourceManager);
            string oldDepartmentName = Console.ReadLine();
            oldDepartmentName = oldDepartmentName.ToUpper();
            while (!humanResourceManager.CheckDepartmentsByName(oldDepartmentName))
            {
                Console.WriteLine("Daxil etdiyiniz departament adi sehvdir. Yeniden daxil edin:");
                oldDepartmentName = Console.ReadLine();
            }
            Console.WriteLine("Secdiyiniz departament ucun yeni ad daxil edin:");
            string newDepartmentName = Console.ReadLine();
            newDepartmentName = newDepartmentName.ToUpper();
            while (humanResourceManager.CheckDepartmentsByName(newDepartmentName) || newDepartmentName.Length < 2)
            {
                Console.WriteLine("Daxil etdiyiniz departament adi duzgun deyil. Basqa ad daxil edin:");
                newDepartmentName = Console.ReadLine();
            }
            humanResourceManager.EditDepartments(ref oldDepartmentName, ref newDepartmentName);

        }
        static void AddEmployee(ref IHumanResourceManager humanResourceManager)
        {
            Departments(humanResourceManager);
            Console.WriteLine("Isci elave etmek istediyiniz departamentin adini daxil edin:");
            string departmentName = Console.ReadLine();
            departmentName = departmentName.ToUpper();
            while (!humanResourceManager.CheckDepartmentsByName(departmentName))
            {
                Console.WriteLine("Duzgun departament adi daxil edin:");
                departmentName = Console.ReadLine();
                departmentName = departmentName.ToUpper();
            }
           /*Bele yazanda ad duz teyin ede bilmir
             Console.WriteLine("Elave etmek istediyiniz iscinin adini daxil edin:");
            string name = Console.ReadLine();
            while (name.Length<2)
             {
                 Console.WriteLine("Duzgun ad daxil edin. Minimum 2 herf olmalidir:");
                 name = Console.ReadLine();
             }
             Console.WriteLine("Elave etmek istediyiniz iscinin soyadini daxil edin:");
             string surname = Console.ReadLine();
             while (surname.Length < 2)
             {
                 Console.WriteLine("Duzgun soyad daxil edin. Minimum 2 herf olmalidir:");
                 surname = Console.ReadLine();
             }

              bele yazanda ad yazan kimi proses diyanr*/
           Console.WriteLine("Elave etmek istediyiniz iscinin adini daxil edin:");
            string name = Console.ReadLine();
            while (true)
              {
                  if (name.Length >= 2)
                  {
                      bool letterCheck = false;
                      foreach (char item in name)
                      {
                          if (char.IsLetter(item))
                          {
                              letterCheck = true;
                              return;
                          }
                      }
                      Console.WriteLine("Duzgun isci adi daxil edin. Isci adi yalniz herflerden ibaret olmalidir:");
                      name = Console.ReadLine();
                      continue;
                  }
                  Console.WriteLine("Duzgun ad daxil edin. Minimum 2 herf olmalidir:");
                  name = Console.ReadLine();

              }
              Console.WriteLine("Elave etmek istediyiniz iscinin soyadini daxil edin:");
              string surname = Console.ReadLine();
              while (true)
              {
                  if (surname.Length >= 2)
                  {
                      bool letterCheck = false;
                      foreach (char item in surname)
                      {
                          if (char.IsLetter(item))
                          {
                              letterCheck = true;
                              return;
                          }
                      }
                      Console.WriteLine("Duzgun isci soyadi daxil edin. Isci soyadi yalniz herflerden ibaret olmalidir:");
                      surname = Console.ReadLine();
                      continue;
                  }
                  Console.WriteLine("Duzgun soyad daxil edin. Minimum 2 herf olmalidir:");
                  surname = Console.ReadLine();
              }
            name = name.Replace(name[0], char.ToUpper(name[0]));
            surname = surname.Replace(surname[0], char.ToUpper(surname[0]));
            StringBuilder fullName = new StringBuilder(name);
            fullName.Append(" ");
            fullName.Append(surname);
            string fullname = fullName.ToString();
            Console.WriteLine("Iscinin vezifesini daxil edin:");
            string position = Console.ReadLine();
            while (position.Length < 2)
            {
                Console.WriteLine("Duzgun vezife daxil edin. Iscinin vezifesi minimum 2 herfden ibaret olmalidir:");
                position = Console.ReadLine();
            }
            Console.WriteLine("Isnin maasini daxil edin:");
            string salaryStr = Console.ReadLine();
            double salary;
            while (!double.TryParse(salaryStr, out salary) || salary < 250)
            {
                Console.WriteLine("Duzgun maas daxil edin:");
                salaryStr = Console.ReadLine();
            }

            humanResourceManager.AddEmployee(departmentName, fullname, position, salary);

        }
        static void EditEmployee(ref IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Isci duzelis etmek istediyiniz departamentin adini daxil edin:");
            Departments(humanResourceManager);
            string departmentName = Console.ReadLine();
            while (!humanResourceManager.CheckDepartmentsByName(departmentName))
            {
                Console.WriteLine("Daxil olunnan adda department movcud deyil. Yeniden daxil edin:");
                departmentName = Console.ReadLine();

            }
            foreach (Department department in humanResourceManager.Departments)
            {
                if (departmentName==department.Name)
                {
                    foreach (Employee employee in department.Employees)
                    {
                        if (department.Employees.Length<0)
                        {
                            Console.WriteLine("Ilk once isci elave edin");
                            return;
                        }
                    }

                }
                Console.WriteLine("Duzgun departament adi daxil edin:");
                departmentName = Console.ReadLine();
            }
            Console.WriteLine("Deyisiklik etmek istediyiniz iscinin ID nomresini daxil edin:");
            string no = Console.ReadLine();
            no = no.ToUpper();
            ShowEmployeesByDepartments(humanResourceManager);
            while (!humanResourceManager.CheckEmployeesByNo(no))
            {
                Console.WriteLine("Duzgun ID daxil edin:");
                no = Console.ReadLine();
                no.ToUpper();
            }
            foreach (Department departments in humanResourceManager.Departments)
            {
                foreach (Employee employee in departments.Employees)
                {
                    if (no.ToUpper() == employee.No)
                    {
                        Console.WriteLine($"Fullname:{employee.Fullname} Salary{employee.Salary} Position{employee.Position}");
                        Console.WriteLine("Yeni maas deyeri daxil edin:");
                        string newSalaryStr = Console.ReadLine();
                        double newSalary;
                        while (!double.TryParse(newSalaryStr, out newSalary)||newSalary==employee.Salary||departments.SalaryLimit< (newSalary+(departments.CalcSalaryAverage()*departments.Employees.Length)))
                        {
                            Console.WriteLine("Duzgun salary deyeri daxil edin:");
                            newSalaryStr = Console.ReadLine();
                        }
                        Console.WriteLine("Yeni position deyeri daxil edin:");
                        string newPosition = Console.ReadLine();
                        while (newPosition.Length<2||newPosition==employee.Position)
                        {
                            Console.WriteLine("Duzgun position deyeri daxil edin:");
                            newPosition = Console.ReadLine();
                        }
                        humanResourceManager.EditEmployee(employee.DepartmentName, employee.No, employee.Fullname, newSalary, newPosition);
                        Console.WriteLine("Yeni deyerler:");
                        Console.WriteLine($"Fullname:{employee.Fullname} Salary{newSalary} Position{newPosition}");
                        return;
                    }
                    
                }
            }
            
        }
        static void RemoveEmployee(ref IHumanResourceManager humanResourceManager)
        {
            Departments(humanResourceManager);
            Console.WriteLine("Isci silmek istediyiniz departamentin adini daxil edin");
            string departmentName = Console.ReadLine();
            while (!humanResourceManager.CheckDepartmentsByName(departmentName))
            {
                Console.WriteLine("Daxil olunnan adda department movcud deyil. Yeniden daxil edin:");
                departmentName = Console.ReadLine();

            }
            foreach (Department department in humanResourceManager.Departments)
            {
                if (departmentName == department.Name)
                {
                    foreach (Employee employee in department.Employees)
                    {
                        if (department.Employees.Length < 0)
                        {
                            Console.WriteLine("Ilk once isci elave edin");
                            return;
                        }
                    }

                }
                Console.WriteLine("Duzgun departament adi daxil edin:");
                departmentName = Console.ReadLine();
            }
            Console.WriteLine("Silmek istediyiniz iscinin ID nomresini daxil edin:");
            string employeeNo = Console.ReadLine();
            employeeNo = employeeNo.ToUpper();
            ShowEmployeesByDepartments(humanResourceManager);
            while (!humanResourceManager.CheckEmployeesByNo(employeeNo))
            {
                Console.WriteLine("Duzgun ID daxil edin:");
                employeeNo = Console.ReadLine();
                employeeNo.ToUpper();
            }
            humanResourceManager.RemoveEmployee(departmentName, employeeNo);
        }
        static void ShowAllEmployees(IHumanResourceManager humanResourceManager)
        {
            foreach (Department department in humanResourceManager.Departments)
            {
                foreach (Employee employee in department.Employees)
                {
                    Console.WriteLine($"ID:{employee.No} DepartmentName:{employee.DepartmentName} Fullname:{employee.Fullname} Position:{employee.Position} Salary:{employee.Salary}");
                }
            }

        }
        static void ShowEmployeesByDepartments(IHumanResourceManager humanResourceManager)
        {
            Console.WriteLine("Departament adi daxil edin:");
            Departments(humanResourceManager);
            string departmentName = Console.ReadLine();
            departmentName = departmentName.ToUpper();
            while (!humanResourceManager.CheckDepartmentsByName(departmentName))
            {
                Console.WriteLine("Daxil olunnan adda department movcud deyil. Yeniden daxil edin:");
                departmentName = Console.ReadLine();

            }
            foreach (Department department in humanResourceManager.Departments)
            {
                if (departmentName == department.Name)
                {
                    foreach (Employee employee in department.Employees)
                    {
                        if (department.Employees.Length < 0)
                        {
                            Console.WriteLine("Secdiyiniz departamentde hec bir isci yoxdur");
                            return;
                        }
                    }

                }
               // Console.WriteLine("Duzgun departament adi daxil edin:");
                //departmentName = Console.ReadLine();     
            }
            foreach (Department departments in humanResourceManager.Departments)
            {
                foreach (Employee employee in departments.Employees)
                {
                    Console.WriteLine($"ID:{employee.No} Fullname:{employee.Fullname} Position:{employee.Position} Salary:{employee.Salary}");

                }
            }
        }
    }
}




