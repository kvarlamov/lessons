namespace HardWork._01___Cyclomatic_Complexity;

internal sealed class Example2_withoutFix
{
    public void ProcessOrders(Customer customer)
    {
        foreach (var order in customer.Orders)
        {
            if (order.OrderType == "Normal")
            {
                if (order.Amount > 1000)
                {
                    Console.WriteLine($"Employee {customer.Name} is processing a high-value normal order: {order.OrderId}");
                    order.IsProcessed = true;
                }
                else if (order.Amount > 500)
                {
                    Console.WriteLine($"Employee {customer.Name} is processing a medium-value normal order: {order.OrderId}");
                    order.IsProcessed = true;
                }
                else
                {
                    Console.WriteLine($"Employee {customer.Name} is processing a low-value normal order: {order.OrderId}");
                    order.IsProcessed = true;
                }
            }
            else if (order.OrderType == "Urgent")
            {
                if (customer.Role == "Manager")
                {
                    Console.WriteLine($"Manager {customer.Name} is processing an urgent order: {order.OrderId}");
                    order.IsProcessed = true;
                }
                else
                {
                    Console.WriteLine($"Employee {customer.Name} is not authorized to process urgent orders: {order.OrderId}");
                    order.IsProcessed = false;
                }
            }
            else if (order.OrderType == "Bulk")
            {
                if (order.Amount > 5000)
                {
                    Console.WriteLine($"Employee {customer.Name} is processing a high-value bulk order: {order.OrderId}");
                    order.IsProcessed = true;
                }
                else
                {
                    for (int i = 0; i < order.Amount / 100; i++)
                    {
                        Console.WriteLine($"Employee {customer.Name} is processing part {i + 1} of bulk order: {order.OrderId}");
                    }
                    order.IsProcessed = true;
                }
            }
            else
            {
                Console.WriteLine($"Employee {customer.Name} encountered an unknown order type: {order.OrderId}");
                order.IsProcessed = false;
            }
        }
    }
}

public class Customer
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public List<Order> Orders { get; set; } = new();
}

public class Order
{
    public int OrderId { get; set; }
    public string OrderType { get; set; }
    public bool IsProcessed { get; set; }
    public double Amount { get; set; }
}