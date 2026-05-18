using System;
using System.Collections.Generic;

namespace RestockFeeSystem
{
    class Program
    {
        // Method to calculate fee
        static double CalculateRestockFee(double itemPrice, int conditionCode)
        {
            double multiplier = 0;

            // Switch statement
            switch (conditionCode)
            {
                case 1:
                    multiplier = 0.0;
                    break;

                case 2:
                    multiplier = 0.10;
                    break;

                case 3:
                    multiplier = 0.25;
                    break;

                case 4:
                    multiplier = 0.50;
                    break;

                default:
                    throw new ArgumentException("Invalid condition code!");
            }

            // Raw fee
            double rawFee = itemPrice * multiplier;

            // Manual rounding to nearest cent
            double temp = rawFee * 100;

            int rounded = (int)temp;

            // if there is remaining decimal
            if (temp > rounded)
            {
                rounded = rounded + 1;
            }

            double finalFee = rounded / 100.0;

            return finalFee;
        }

        // Process all returns
        static double ProcessReturns(List<double> prices, List<int> conditions)
        {
            double totalFees = 0;

            for (int i = 0; i < prices.Count; i++)
            {
                double fee = CalculateRestockFee(prices[i], conditions[i]);

                totalFees += fee;
            }

            return totalFees;
        }

        static void Main(string[] args)
        {
            // Sample data
            List<double> prices = new List<double>()
            {
                120.75,
                89.99,
                45.50,
                100.10
            };

            List<int> conditions = new List<int>()
            {
                2,
                3,
                4,
                7 // invalid code intentionally
            };

            bool logFileOpened = false;

            try
            {
                // simulate opening log file
                logFileOpened = true;

                Console.WriteLine("Log file opened.");

                double total = ProcessReturns(prices, conditions);

                Console.WriteLine("Total Fees: $" + total);
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine("Error: " + ex.Message);
            }
            finally
            {
                // close resource
                logFileOpened = false;

                Console.WriteLine("Log file closed.");
            }

            Console.ReadLine();
        }
    }
}