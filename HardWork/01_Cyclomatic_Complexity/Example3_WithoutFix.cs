namespace HardWork._01_Cyclomatic_Complexity;

internal sealed class Example3_WithoutFix
{
    public class Customer
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Transaction> Transactions { get; set; }
    }

public class Transaction
{
    public int TransactionId { get; set; }
    public string Type { get; set; }
    public double Amount { get; set; }
    public DateTime Date { get; set; }
}

public class TransactionProcessor
{
    public void ProcessTransactions(Customer customer)
    {
        if (customer == null)
        {
            Console.WriteLine("Customer is null.");
            return;
        }

        if (customer.Transactions == null)
        {
            Console.WriteLine("Customer transactions are null.");
            return;
        }

        foreach (var transaction in customer.Transactions)
        {
            if (transaction == null)
            {
                Console.WriteLine("Found a null transaction.");
                continue;
            }

            if (transaction.Type == "Purchase")
            {
                Console.WriteLine($"purchase transaction {transaction.TransactionId} on {transaction.Date}");
            }
            else if (transaction.Type == "Refund")
            {
                Console.WriteLine($"refund transaction {transaction.TransactionId} on {transaction.Date}");
            }
            else if (transaction.Type == "Withdrawal")
            {
                for (int i = 0; i < transaction.Amount / 100; i++)
                {
                    Console.WriteLine($"Processing part {i + 1} of withdrawal transaction {transaction.TransactionId}");
                }
            }
            else
            {
                Console.WriteLine($"Unknown transaction type {transaction.TransactionId} on {transaction.Date}");
            }
        }
    }
}
}