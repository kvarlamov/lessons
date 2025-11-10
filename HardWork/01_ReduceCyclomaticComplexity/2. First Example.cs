using System.Text;

namespace HardWork._01_ReduceCyclomaticComplexity;

public class Program
{
    public static void Main()
    {   
        OrderProcessor.ProcessOrder(new  Order());
    }
}

#region Before

public class Order
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public string CustomerType { get; set; }
    public string PaymentMethod { get; set; }
    public bool IsInternational { get; set; }
    public int ItemCount { get; set; }
    public DateTime OrderDate { get; set; }
}

public class OrderProcessor
{
    public static string ProcessOrder(Order order)
    {
        string result = "Order processed: ";
        
        // Множественные условия и вложенные if
        if (order.TotalAmount > 1000)
        {
            if (order.CustomerType == "VIP")
            {
                result += "VIP discount applied. ";
                if (order.PaymentMethod == "CreditCard")
                {
                    result += "Extra 5% cashback. ";
                }
            }
            else if (order.CustomerType == "Regular")
            {
                result += "Standard processing. ";
            }
        }
        else if (order.TotalAmount > 500)
        {
            result += "Medium order. ";
            if (order.ItemCount > 10)
            {
                result += "Bulk items. ";
            }
        }
        else
        {
            result += "Small order. ";
        }

        // Дополнительные условия
        if (order.IsInternational)
        {
            result += "International shipping. ";
            if (order.TotalAmount > 2000)
            {
                result += "Free international shipping. ";
            }
            else
            {
                result += "Shipping fees apply. ";
            }
        }
        else
        {
            result += "Domestic shipping. ";
        }

        // Проверка метода оплаты
        switch (order.PaymentMethod)
        {
            case "CreditCard":
                result += "Credit card payment.";
                break;
            case "PayPal":
                result += "PayPal payment.";
                break;
            case "BankTransfer":
                result += "Bank transfer.";
                if (order.TotalAmount > 5000)
                {
                    result += " Verification required.";
                }
                break;
            default:
                result += "Unknown payment method.";
                break;
        }

        return result;
    }
}

#endregion

#region After

/* Исходная ЦС метода ProcessOrder составляла 23
 * Благодаря рефакторингу её удалось снизить до 6
 * Использованные приёмы:
 * 1) выделение вложенной в if логики в отдельные методы
 * -- выделил логикуметоды обработки заказа -- несмотря на то, что это самый простой приём и в реальном
 * продакшн коде применять не очень эффективно, даже это привело к снижению сложности и улучшению читаемости
 *
 * 2) полиморфизм
 * -- строковые поля CustomerType и PaymentMethod были переделаны в типы и добавлены через композицию
 * в тип Order, что позволило определять логику обработки конкретного типа полиморфно - через общий метод
 *
 * Вывод: для этого примера я решил ограничиться этим манипуляциями и даже их хватило, чтобы снизить ЦС почти в 4 раза
 */ 

public class OrderV2
{
    public int Id { get; set; }
    public decimal TotalAmount { get; set; }
    public CustomerType CustomerType { get; set; }
    public Payment PaymentMethod { get; set; }
    public bool IsInternational { get; set; }
    public int ItemCount { get; set; }
    public DateTime OrderDate { get; set; }

    public void ApplyDiscount()
    {
        TotalAmount *= (decimal)CustomerType.DiscountModifier;
    }
}

public class OrderResult
{
    private StringBuilder OrderMessage { get; } = new();

    public void AppendMessage(string orderProcessed)
    {
        OrderMessage.AppendLine(orderProcessed);
    }

    public void PrintResult()
    {
        OrderMessage.AppendLine();
    }
}

public abstract class Payment
{
    public abstract void Process();
}

public sealed class CreditCardPayment : Payment
{
    public override void Process()
    {
        throw new NotImplementedException();
    }
}

public sealed class PayPalPayment : Payment
{
    public override void Process()
    {
        throw new NotImplementedException();
    }
}

public sealed class BankTransferPayment : Payment
{
    public override void Process()
    {
        throw new NotImplementedException();
    }
}

public abstract class CustomerType
{
    public abstract double DiscountModifier { get; }
}

public sealed class VipCustomerType : CustomerType
{
    public override double DiscountModifier => 0.5;
}

public sealed class RegisteredCustomerType : CustomerType
{
    public override double DiscountModifier => 0.9;
}

public class OrderProcessorV2
{
    public OrderResult ProcessOrder(OrderV2 order)
    {
        var result = new OrderResult();
        result.AppendMessage("Order processed: ");
        
        switch (order.TotalAmount)
        {
            case > 1000:
                HandleBigOrder(order, result);
                break;
            case > 500:
                HandleMediumOrder(order, result);
                break;
            default:
                result.AppendMessage("Small order. ");
                break;
        }

        
        // Дополнительные условия
        if (order.IsInternational)
        {
            HandleInternationalOrder(order, result);
        }
        else
        {
            result.AppendMessage("Domestic shipping. ");
        }

        order.PaymentMethod.Process();

        return result;
    }

    private void HandleInternationalOrder(OrderV2 order, OrderResult result)
    {
        result.AppendMessage("International shipping. ");
        result.AppendMessage(order.TotalAmount > 2000 
            ? "Free international shipping. " 
            : "Shipping fees apply. ");
    }

    private void HandleMediumOrder(OrderV2 order, OrderResult result)
    {
        result.AppendMessage("Medium order. ");
        if (order.ItemCount > 10)
        {
            result.AppendMessage("Bulk items. ");
        }
    }

    private void HandleBigOrder(OrderV2 order, OrderResult result)
    {
        order.ApplyDiscount();
        order.PaymentMethod.Process();
    }
}

#endregion