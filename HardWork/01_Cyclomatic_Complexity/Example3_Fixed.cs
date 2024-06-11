namespace HardWork._01_Cyclomatic_Complexity;

internal sealed class Example3_Fixed
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<BaseTransaction> Transactions { get; } = new();
    }
    
    public abstract class BaseTransaction
    {
        public int TransactionId { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        
        public abstract void Process();
    }
    
    public class NullTransaction : BaseTransaction
    {
        public override void Process()
        {
            Console.WriteLine("Null transaction cannot be handler.");
        }
    }
    
    public class Purchase : BaseTransaction
    {
        public override void Process()
        {
            Console.WriteLine($"purchase transaction {TransactionId} on {Date}");
        }
    }
    
    public class Refund : BaseTransaction
    {
        public override void Process()
        {
            Console.WriteLine($"refund transaction {TransactionId} on {Date}");
        }
    }
    
    public class Withdraw : BaseTransaction
    {
        public override void Process()
        {
            for (var i = 0; i < Amount / 100; i++)
                Console.WriteLine(
                    $"Processing part {i + 1} of withdrawal transaction {TransactionId}");
        }
    }
    
    public class TransactionProcessor
    {
        public void ProcessTransactions(IReadOnlyCollection<BaseTransaction> transactions)
        {
            foreach (var transaction in transactions)
            {
                transaction.Process();
            }
        }
    }
}