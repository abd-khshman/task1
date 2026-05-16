using System;
using System.Collections.Generic;

namespace EmployeeOvertimeSystem
{
    // Custom Exception Class
    public class InvalidShiftDataException : Exception
    {
        public InvalidShiftDataException(string message) : base(message)
        {
        }
    }

    // Employee Class
    class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; }
        public double HourlyRate { get; set; }
        public double HoursWorked { get; set; }
        public double Bonus { get; set; }

        public Employee(int id, string name, double rate, double hours, double bonus = 0)
        {
            EmployeeId = id;
            Name = name;
            HourlyRate = rate;
            HoursWorked = hours;
            Bonus = bonus;
        }
    }

    class Program
    {
        // Method 1: Calculate pay without bonus
        static double CalculateWeeklyPay(double hourlyRate, double hoursWorked)
        {
            // Validation
            if (hourlyRate <= 0 || hoursWorked < 0 || hoursWorked > 168)
            {
                throw new InvalidShiftDataException(
                    "Invalid data: Hourly rate must be > 0 and hours must be between 0 and 168."
                );
            }

            double totalPay = 0;

            // Regular Hours
            if (hoursWorked <= 40)
            {
                totalPay = hoursWorked * hourlyRate;
            }

            // Overtime (40 - 60)
            else if (hoursWorked <= 60)
            {
                double regularPay = 40 * hourlyRate;
                double overtimeHours = hoursWorked - 40;
                double overtimePay = overtimeHours * hourlyRate * 1.5;

                totalPay = regularPay + overtimePay;
            }

            // Double Overtime (> 60)
            else
            {
                double regularPay = 40 * hourlyRate;

                double overtimeHours = 20;
                double overtimePay = overtimeHours * hourlyRate * 1.5;

                double doubleOvertimeHours = hoursWorked - 60;
                double doubleOvertimePay = doubleOvertimeHours * hourlyRate * 2.0;

                totalPay = regularPay + overtimePay + doubleOvertimePay;
            }

            return totalPay;
        }

        // Method Overloading: Calculate pay with bonus
        static double CalculateWeeklyPay(double hourlyRate, double hoursWorked, double bonus)
        {
            double totalPay = CalculateWeeklyPay(hourlyRate, hoursWorked);
            return totalPay + bonus;
        }

        static void Main(string[] args)
        {
            // Employee Collection
            List<Employee> employees = new List<Employee>()
            {
                new Employee(1, "Ahmad", 10, 35),      // Regular
                new Employee(2, "Sara", 15, 40),       // Edge Case 40
                new Employee(3, "Omar", 20, 55),       // Overtime
                new Employee(4, "Lina", 25, 65, 100),  // Double Overtime + Bonus
                new Employee(5, "Ali", -5, 30),        // Invalid Rate
                new Employee(6, "Noor", 18, 170)       // Invalid Hours
            };

            // Loop Through Employees
            foreach (Employee emp in employees)
            {
                try
                {
                    double totalPay;

                    // Check if bonus exists
                    if (emp.Bonus > 0)
                    {
                        totalPay = CalculateWeeklyPay(
                            emp.HourlyRate,
                            emp.HoursWorked,
                            emp.Bonus
                        );
                    }
                    else
                    {
                        totalPay = CalculateWeeklyPay(
                            emp.HourlyRate,
                            emp.HoursWorked
                        );
                    }

                    // Print Success Result
                    Console.WriteLine(
                        $"Employee ID: {emp.EmployeeId}, " +
                        $"Name: {emp.Name}, " +
                        $"Total Pay: {totalPay:F2}"
                    );
                }
                catch (InvalidShiftDataException ex)
                {
                    // Print Error Message
                    Console.WriteLine(
                        $"Error for Employee {emp.EmployeeId} ({emp.Name}): {ex.Message}"
                    );
                }
            }

            Console.WriteLine("\nProcessing Completed.");
        }
    }
}