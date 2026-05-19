using System;
using System.Collections.Generic;

namespace ShippingCalculator
{
    // Step 2: Custom Exception
    class InvalidOrderException : Exception
    {
        public InvalidOrderException(string message) : base(message)
        {
        }
    }

    // Order class
    class Order
    {
        public int OrderId { get; set; }
        public double Weight { get; set; }
        public string DestinationZone { get; set; }
        public bool IsExpress { get; set; }
        public int DiscountPercent { get; set; }
    }

    class Program
    {
        // Step 3: Main shipping cost method
        static double CalculateShippingCost(double weight, string destinationZone, bool isExpress)
        {
            // Step 4: Validation
            if (weight <= 0)
            {
                throw new InvalidOrderException("Weight must be greater than 0.");
            }

            if (destinationZone != "Local" &&
                destinationZone != "Regional" &&
                destinationZone != "National" &&
                destinationZone != "International")
            {
                throw new InvalidOrderException("Invalid destination zone.");
            }

            double baseRate = 0;

            // Step 5: Determine base rate
            switch (destinationZone)
            {
                case "Local":
                    baseRate = 2.0;
                    break;

                case "Regional":
                    baseRate = 5.0;
                    break;

                case "National":
                    baseRate = 10.0;
                    break;

                case "International":
                    baseRate = 25.0;
                    break;
            }

            // Step 6: Calculate total cost
            double totalCost = weight * baseRate;

            if (isExpress)
            {
                totalCost *= 1.5;
            }

            return totalCost;
        }

        // Step 7: Overloaded method with discount
        static double CalculateShippingCost(double weight, string destinationZone, bool isExpress, int discountPercent)
        {
            double totalCost = CalculateShippingCost(weight, destinationZone, isExpress);

            totalCost -= totalCost * discountPercent / 100.0;

            return totalCost;
        }

        static void Main(string[] args)
        {
            // Step 8: Mock orders
            List<Order> orders = new List<Order>()
            {
                new Order { OrderId = 101, Weight = 5.5, DestinationZone = "Local", IsExpress = false, DiscountPercent = 0 },
                new Order { OrderId = 102, Weight = 3.2, DestinationZone = "Regional", IsExpress = true, DiscountPercent = 10 },
                new Order { OrderId = 103, Weight = 8.0, DestinationZone = "National", IsExpress = false, DiscountPercent = 5 },
                new Order { OrderId = 104, Weight = 2.5, DestinationZone = "International", IsExpress = true, DiscountPercent = 0 },
                new Order { OrderId = 105, Weight = -4.0, DestinationZone = "Local", IsExpress = false, DiscountPercent = 0 }, // Invalid weight
                new Order { OrderId = 106, Weight = 6.0, DestinationZone = "Interntional", IsExpress = true, DiscountPercent = 0 } // Invalid zone
            };

            // Step 9: Loop through orders
            foreach (Order order in orders)
            {
                try
                {
                    double calculatedCost;

                    // Step 7: Call overloaded method if discount exists
                    if (order.DiscountPercent > 0)
                    {
                        calculatedCost = CalculateShippingCost(
                            order.Weight,
                            order.DestinationZone,
                            order.IsExpress,
                            order.DiscountPercent
                        );
                    }
                    else
                    {
                        calculatedCost = CalculateShippingCost(
                            order.Weight,
                            order.DestinationZone,
                            order.IsExpress
                        );
                    }

                    // Step 10: Success message
                    Console.WriteLine(
                        $"Order ID: {order.OrderId} | Final Cost: ${calculatedCost:F2}"
                    );
                }
                catch (InvalidOrderException ex)
                {
                    // Step 10: Error handling
                    Console.WriteLine(
                        $"Order ID: {order.OrderId} failed: {ex.Message}"
                    );
                }
            }

            Console.WriteLine("\nBatch processing completed.");
        }
    }
}