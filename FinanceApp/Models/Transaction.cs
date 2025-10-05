namespace FinanceApp.Models
{
    public enum TransactionType
    {
        Income,
        Expense
    }
    public sealed class Transaction
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public DateTime Date { get; init; }
        public decimal Amount { get; init; } // positive value
        public TransactionType Type { get; init; }
        public string? Description { get; init; }

        
        public Transaction(DateTime date, decimal amount, TransactionType type, string? description = null)
        {
            if (amount <= 0) throw new ArgumentOutOfRangeException(nameof(amount), "Число должно быть положительным.");
            Date = date;
            Amount = amount;
            Type = type;
            Description = description;
        }
        public override string ToString()
        {
            var amountSign = Type == TransactionType.Income ? "+" : "-";
            return $"{Date:dd-MM-yyyy} {amountSign}{Amount:N2} {Description}";
        }
    }
}