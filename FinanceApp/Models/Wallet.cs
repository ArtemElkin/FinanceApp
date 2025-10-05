namespace FinanceApp.Models
{
    public sealed class Wallet
    {
        public Guid Id { get; init; } = Guid.NewGuid();
        public string Name { get; init; }
        public string Currency { get; init; }
        public decimal InitialBalance { get; init; }

        private readonly List<Transaction> _transactions = new();
        public IReadOnlyList<Transaction> Transactions => _transactions;

        public Wallet(string name, string currency, decimal initialBalance = 0m)
        {
            Name = name ?? throw new ArgumentNullException(nameof(name));
            Currency = currency ?? throw new ArgumentNullException(nameof(currency));
            InitialBalance = initialBalance;
        }

        public decimal CurrentBalance =>
            InitialBalance
            + _transactions.Where(t => t.Type == TransactionType.Income).Sum(t => t.Amount)
            - _transactions.Where(t => t.Type == TransactionType.Expense).Sum(t => t.Amount);
        
        public void AddTransaction(Transaction transaction)
        {
            if (transaction.Type == TransactionType.Expense && transaction.Amount > CurrentBalance)
            {
                throw new InvalidOperationException(
                    $"Недостаточно средств в кошельке '{Name}" +
                    $"' для добавления расхода на {transaction.Amount:N2} {Currency}.");
            }
            _transactions.Add(transaction);
        }

        public override string ToString() => $"{Name} ({Currency}) — Текущий баланс: {CurrentBalance:N2}";
    }
}