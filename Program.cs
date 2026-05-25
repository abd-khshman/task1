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

    // Shipment Model
    public class Shipment
    {
        public int OrderId;
        public double WeightKg;
        public string DestinationZone;
        public bool IsExpress;
        public int DiscountPercent;

        public Shipment(int orderId, double weightKg, string destinationZone, bool isExpress, int discountPercent = 0)
        {
            OrderId = orderId;
            WeightKg = weightKg;
            DestinationZone = destinationZone;
            IsExpress = isExpress;
            DiscountPercent = discountPercent;
        }
    }

    class Program
    {
        // First overloaded method
        static double CalculateShippingCost(double weightKg, string destinationZone, bool isExpress)
        {
            // Validation
            if (weightKg <= 0)
            {
                throw new InvalidShipmentException("Weight must be greater than 0.");
            }

            if (destinationZone != "Domestic" &&
                destinationZone != "International" &&
                destinationZone != "Remote")
            {
                throw new InvalidShipmentException("Invalid destination zone.");
            }

            double baseRate = 0;

            // Zone pricing
            if (destinationZone == "Domestic")
            {
                baseRate = 5.00;
            }
            else if (destinationZone == "International")
            {
                baseRate = 12.00;
            }
            else
            {
                baseRate = 20.00;
            }

            // Cost calculation
            double totalCost = weightKg * baseRate;

            // Express shipping
            if (isExpress == true)
            {
                totalCost = totalCost * 1.5;
            }

            return totalCost;
        }

        // Second overloaded method with discount
        static double CalculateShippingCost(double weightKg, string destinationZone, bool isExpress, int discountPercent)
        {
            double totalCost = CalculateShippingCost(weightKg, destinationZone, isExpress);

            if (discountPercent > 0)
            {
                totalCost = totalCost - (totalCost * discountPercent / 100.0);
            }

            return totalCost;
        }

        static void Main(string[] args)
        {
            // List of shipments
            List<Shipment> shipments = new List<Shipment>()
            {
                new Shipment(1001, 10.5, "Domestic", false),
                new Shipment(1002, 5.2, "International", true),
                new Shipment(1003, 3.0, "Remote", true, 10),
                new Shipment(1004, -4.5, "Domestic", false), // Invalid weight
                new Shipment(1005, 7.0, "domestic", true),   // Invalid zone (case-sensitive)
                new Shipment(1006, 12.5, "International", false, 20)
            };

            foreach (Shipment shipment in shipments)
            {
                try
                {
                    double calculatedCost = 0;

                    // Call correct overloaded method
                    if (shipment.DiscountPercent > 0)
                    {
                        calculatedCost = CalculateShippingCost(
                            shipment.WeightKg,
                            shipment.DestinationZone,
                            shipment.IsExpress,
                            shipment.DiscountPercent
                        );
                    }
                    else
                    {
                        calculatedCost = CalculateShippingCost(
                            shipment.WeightKg,
                            shipment.DestinationZone,
                            shipment.IsExpress
                        );
                    }

                    Console.WriteLine(
                        $"Order ID: {shipment.OrderId} | Shipping Cost: ${calculatedCost:F2}"
                    );
                }
                catch (InvalidShipmentException ex)
                {
                    Console.WriteLine(
                        $"Error in Order ID {shipment.OrderId}: {ex.Message}"
                    );
                }
            }

            // ---------------------------------------------------
            // INTENTIONAL MISTAKES FOR TESTING THE COMPILER
            // Uncomment ONE at a time to test compiler errors
            // ---------------------------------------------------

            // 1. Type mismatch error
            // int wrongValue = "Hello";

            // 2. Missing semicolon
            // Console.WriteLine("Compiler Test")

            // 3. Undefined variable
            // Console.WriteLine(totalPrice);

            // 4. Wrong parameter type
            // double test = CalculateShippingCost("5", "Domestic", true);

            // 5. Invalid logical comparison
            // if (5 = 10)
            // {
            //     Console.WriteLine("Error");
            // }

            Console.WriteLine("\nProcessing Finished.");
        }
    }
}