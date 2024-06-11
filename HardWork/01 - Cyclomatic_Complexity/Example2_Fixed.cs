namespace HardWork._01___Cyclomatic_Complexity;

public class CustomerFixed
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Role { get; set; }
    public List<BaseOrder> Orders { get; set; } = new();
}

internal sealed class Example2_Fixed
{
    public void ProcessOrders(List<BaseOrder> orders)
    {
        foreach (var order in orders)
        {
            order.GeneralOrderProcess();
        }
    }
}

public abstract class BaseOrder
{
    public void GeneralOrderProcess()
    {
        Process();
        ProcessOrderAmountStrategy();
    }

    private void ProcessOrderAmountStrategy()
    {
        var strategy = OrderAmountFactory.GetStrategy(OrderAmountType);
        strategy.ProcessOrderAmount(this);
    }

    public abstract void Process();
    public int OrderId { get; set; }
    public bool IsProcessed { get; set; }
    public OrderAmountType OrderAmountType { get; set; }
}

public enum OrderAmountType
{
    None = 0,
    Low = 1,
    Medium = 2,
    High = 3
}

public static class OrderAmountFactory
{
    public static IOrderAmountStrategy GetStrategy(OrderAmountType amountType)
    {
        return amountType switch
        {
            OrderAmountType.Low => new LowAmountStrategy(),
            OrderAmountType.Medium => new MediumAmountStrategy(),
            OrderAmountType.High => new HighAmountStrategy(),
            _ => throw new ArgumentException("Wrong argument {0}", nameof(amountType))
        };
    }
}

public interface IOrderAmountStrategy
{
    public void ProcessOrderAmount(BaseOrder order);
}

public sealed class HighAmountStrategy : IOrderAmountStrategy
{
    public void ProcessOrderAmount(BaseOrder order)
    {
        Console.WriteLine("Process high amount order");
    }
}

public sealed class MediumAmountStrategy : IOrderAmountStrategy
{
    public void ProcessOrderAmount(BaseOrder order)
    {
        Console.WriteLine("Process medium amount order");
    }
}

public sealed class LowAmountStrategy : IOrderAmountStrategy
{
    public void ProcessOrderAmount(BaseOrder order)
    {
        Console.WriteLine("Process low amount order");
    }
}


public class NullOrder : BaseOrder
{
    public override void Process()
    {
        Console.WriteLine($"encountered an unknown order type: {OrderId}");
        IsProcessed = false;
    }
}

public class NormalOrder : BaseOrder
{
    public override void Process()
    {
        Console.WriteLine("Processing normal order");
    }
}

public class UrgentOrder : BaseOrder
{
    public override void Process()
    {
        Console.WriteLine("Processing urgent order");
    }
}

public class BulkOrder : BaseOrder
{
    public override void Process()
    {
        Console.WriteLine("Processing bulk order");
    }
}