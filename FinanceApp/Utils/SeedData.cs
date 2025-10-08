using FinanceApp.Models;

namespace FinanceApp.Utils
{
    public static class SeedData
    {
        public static List<Wallet> CreateSampleWallets(int transactionsCountPerWallet)
        {
            var w1 = new Wallet("Основной кошелёк", "RUB", 10000m);
            var w2 = new Wallet("Запасной кошелёк", "USD", 20000m);
            List<Wallet> wallets = [w1, w2];
            Random rnd = new Random();
            foreach (var wallet in wallets)
            {
                for (int i = 0; i < transactionsCountPerWallet; i++)
                {
                    DateTime randomDate = new DateTime(rnd.Next(2024, 2026), rnd.Next(1, 13), rnd.Next(1, 29));
                    decimal randomAmount = rnd.Next(1, 10000);
                    TransactionType randomTransactionType = rnd.NextSingle() > 0.5f ? TransactionType.Expense : TransactionType.Income;
                    Transaction transaction = new Transaction(randomDate, randomAmount, randomTransactionType, $"Generated N={i}");
                    try
                    {
                        wallet.AddTransaction(transaction);
                    }
                    catch (InvalidOperationException)
                    {
                        Console.WriteLine("Недостаточно средств для совершения операции:\n" + transaction.ToString());
                    }
                }
            }
            return wallets;
        }
    }
}