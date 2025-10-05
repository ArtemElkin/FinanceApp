using FinanceApp.Models;

namespace FinanceApp.Services
{
    public sealed class FinanceService
    {
        private readonly List<Wallet> _wallets;

        public FinanceService(List<Wallet> wallets)
        {
            _wallets = wallets ?? throw new ArgumentNullException(nameof(wallets));
        }

        public List<Wallet> GetAllWallets() => _wallets;

        /// <summary>
        /// Распределяет транзакции в 2 группы (Income/Expense).
        /// Транзакции внутри группы отсортированы по дате.
        /// </summary>
        public IDictionary<TransactionType, List<Transaction>> GetGroupedTransactionsForWallet(Wallet wallet, int year, int month)
        {
            var transactions = wallet.Transactions
                .Where(t => t.Date.Year == year && t.Date.Month == month)
                .ToList();

            var grouped = transactions
                .GroupBy(t => t.Type)
                .ToDictionary(
                    g => g.Key,
                    g => g.OrderBy(t => t.Date).ToList()
                );

            return grouped;
        }

        /// <summary>
        /// Возвращает пары, отсортированые по сумме расхода по убыванию.
        /// Если расходов меньше 3 — возвращает сколько есть.
        /// </summary>
        public List<(Wallet Wallet, List<Transaction> TopExpenses)> GetTopExpensesPerWallet(int year, int month, int top = 3)
        {
            var result = new List<(Wallet Wallet, List<Transaction> TopExpenses)>();

            foreach (var wallet in _wallets)
            {
                var expenses = wallet.Transactions
                    .Where(t => t.Type == TransactionType.Expense && t.Date.Year == year && t.Date.Month == month)
                    .OrderByDescending(t => t.Amount)
                    .Take(top)
                    .ToList();

                result.Add((wallet, expenses));
            }

            return result;
        }

        /// <summary>
        /// Возвращает для кошелька суммарные Income/Expense за месяцы (используется для сортировки групп по сумме).
        /// </summary>
        public IDictionary<TransactionType, decimal> GetMonthlySums(Wallet wallet, int year, int month)
        {
            var txs = wallet.Transactions.Where(t => t.Date.Year == year && t.Date.Month == month);
            return Enum.GetValues<TransactionType>()
                .ToDictionary(
                    type => type,
                    type => txs.Where(t => t.Type == type).Sum(t => t.Amount)
                );
        }
    }
}
