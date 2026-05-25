using System;
using System.Collections.Generic;

namespace ShippingCalculatorApp
{
    // Custom Exception
    public class InvalidShipmentException : Exception
    {
        public InvalidShipmentException(string message) : base(message)
        {
        }
    }

    class Program
    {
        // Shipment variables class
        class Shipment
        {
            public int OrderId;
            public double WeightKg;
            public string DestinationZone;
            public bool IsExpress;
        }

        // Method to calculate shipping cost
        static double CalculateShippingCost(double weightKg, string destinationZone, bool isExpress)
        {
            // Validation
            if (weightKg <= 0)
            {
                throw new InvalidShipmentException("Invalid Weight");
            }

            // INTENTIONALLY WEAK VALIDATION
            // Missing proper validation for "Remote"
            if (destinationZone != "Domestic" &&
                destinationZone != "International")
            {
                throw new InvalidShipmentException("Invalid Zone");
            }

            double baseRate = 0;

            // Conditional statements
            if (destinationZone == "Domestic")
            {
                baseRate = 5;
            }
            else if (destinationZone == "International")
            {
                baseRate = 12;
            }
            else
            {
                baseRate = 20;
            }

            // Arithmetic operators
            double totalCost = weightKg * baseRate;

            // Express logic
            if (isExpress)
            {
                totalCost = totalCost * 1.5;
            }

            return totalCost;
        }

        static void Main(string[] args)
        {
            // List of shipments
            List<Shipment> shipments = new List<Shipment>()
            {
                new Shipment { OrderId = 1, WeightKg = 10, DestinationZone = "Domestic", IsExpress = false },
                new Shipment { OrderId = 2, WeightKg = 5, DestinationZone = "International", IsExpress = true },
                new Shipment { OrderId = 3, WeightKg = -2, DestinationZone = "Domestic", IsExpress = false },
                new Shipment { OrderId = 4, WeightKg = 3, DestinationZone = "domestic", IsExpress = true },
                new Shipment { OrderId = 5, WeightKg = 6, DestinationZone = "Remote", IsExpress = false }
            };

            // Loop
            foreach (Shipment shipment in shipments)
            {
                try
                {
                    double cost = CalculateShippingCost(
                        shipment.WeightKg,
                        shipment.DestinationZone,
                        shipment.IsExpress
                    );

                    Console.WriteLine("Order ID: " + shipment.OrderId +
                                      " | Cost: $" + cost);
                }
                catch (InvalidShipmentException ex)
                {
                    Console.WriteLine("Error in Order " +
                                      shipment.OrderId +
                                      ": " + ex.Message);
                }
            }

            Console.WriteLine("Finished Processing");
        }
    }
}