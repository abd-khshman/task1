using System;
using System.Collections.Generic;

namespace EmployeeBonusSystem
{
    // Custom Exception
    class InvalidEmployeeDataException : Exception
    {
        public InvalidEmployeeDataException(string message) : base(message)
        {
        }
    }

    class Employee
    {
        public int EmployeeId;
        public string Name;
        public int YearsOfService;
        public double Salary;
        public int PerformanceRating;
        public bool IsExecutive;
    }

    class Program
    {
        // Main CalculateBonus Method
        static double CalculateBonus(double salary, int yearsOfService, int performanceRating)
        {
            // Validation
            if (salary < 0 || yearsOfService < 0 || performanceRating < 1 || performanceRating > 5)
            {
                throw new InvalidEmployeeDataException("Invalid employee data detected.");
            }

            double bonus = 0;

            // Bonus percentage
            if (yearsOfService < 1)
            {
                bonus = 0;
            }
            else if (yearsOfService >= 1 && yearsOfService <= 2)
            {
                bonus = salary * 0.05;
            }
            else if (yearsOfService >= 3 && yearsOfService <= 5)
            {
                bonus = salary * 0.10;
            }
            else
            {
                bonus = salary * 0.15;
            }

            // Performance adjustment
            if (performanceRating < 3)
            {
                bonus = bonus * 0.5;
            }
            else if (performanceRating == 5)
            {
                bonus = bonus * 2;
            }

            return bonus;
        }

        // Overloaded Method
        static double CalculateBonus(double salary, int yearsOfService, int performanceRating, bool isExecutive)
        {
            double finalBonus = CalculateBonus(salary, yearsOfService, performanceRating);

            if (isExecutive == true)
            {
                finalBonus += 1000;
            }

            return finalBonus;
        }

        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>()
            {
                new Employee(){ EmployeeId = 1, Name = "Ahmad", YearsOfService = 2, Salary = 5000, PerformanceRating = 4, IsExecutive = false },

                new Employee(){ EmployeeId = 2, Name = "Sara", YearsOfService = 6, Salary = 8000, PerformanceRating = 5, IsExecutive = true },

                new Employee(){ EmployeeId = 3, Name = "Omar", YearsOfService = 0, Salary = 4500, PerformanceRating = 2, IsExecutive = false },

                // Invalid salary
                new Employee(){ EmployeeId = 4, Name = "Lina", YearsOfService = 3, Salary = -3000, PerformanceRating = 4, IsExecutive = false },

                // Invalid rating
                new Employee(){ EmployeeId = 5, Name = "Khaled", YearsOfService = 4, Salary = 7000, PerformanceRating = 6, IsExecutive = true },

                new Employee(){ EmployeeId = 6, Name = "Rama", YearsOfService = 8, Salary = 9000, PerformanceRating = 5, IsExecutive = true }
            };

            foreach (Employee emp in employees)
            {
                try
                {
                    double calculatedBonus;

                    // Executive check
                    if (emp.IsExecutive)
                    {
                        calculatedBonus = CalculateBonus(emp.Salary, emp.YearsOfService, emp.PerformanceRating, emp.IsExecutive);
                    }
                    else
                    {
                        calculatedBonus = CalculateBonus(emp.Salary, emp.YearsOfService, emp.PerformanceRating);
                    }

                    Console.WriteLine("Employee ID: " + emp.EmployeeId);
                    Console.WriteLine("Name: " + emp.Name);
                    Console.WriteLine("Bonus: $" + calculatedBonus.ToString("F2"));
                    Console.WriteLine("------------------------");
                }
                catch (InvalidEmployeeDataException ex)
                {
                    Console.WriteLine("Error with employee: " + emp.Name);
                    Console.WriteLine(ex.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Unexpected Error: " + ex.Message);
                }
            }

            Console.ReadLine();
        }
    }
}