using FinanceApp.Models;

namespace FinanceApp.Utils
{
    public static class SeedData
    {
        public static List<Wallet> CreateSampleWallets()
        {
            var w1 = new Wallet("Основной кошелёк", "RUB", 10000m);
            var w2 = new Wallet("Запасной кошелёк", "USD", 200m);

            w1.AddTransaction(new Transaction(new DateTime(2025, 9, 1), 111m, TransactionType.Income, "Зарплата"));
            w1.AddTransaction(new Transaction(new DateTime(2025, 9, 2), 222m, TransactionType.Expense, "Продукты"));
            w1.AddTransaction(new Transaction(new DateTime(2025, 9, 3), 333m, TransactionType.Expense, "Такси"));
            w1.AddTransaction(new Transaction(new DateTime(2025, 9, 4), 444m, TransactionType.Expense, "Ресторан"));
            w1.AddTransaction(new Transaction(new DateTime(2025, 8, 5), 555m, TransactionType.Expense, "Ремонт"));

            w2.AddTransaction(new Transaction(new DateTime(2025, 9, 6), 666m, TransactionType.Income, "Фриланс"));
            w2.AddTransaction(new Transaction(new DateTime(2025, 9, 7), 777m, TransactionType.Expense, "Подписка"));
            w2.AddTransaction(new Transaction(new DateTime(2025, 9, 8), 888m, TransactionType.Income, "Возврат"));

            return new List<Wallet> { w1, w2 };
        }
    }
}