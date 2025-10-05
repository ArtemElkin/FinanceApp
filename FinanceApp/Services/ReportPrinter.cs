using FinanceApp.Models;

namespace FinanceApp.Services;

public class ReportPrinter
{
    private readonly FinanceService _service;

    public ReportPrinter(FinanceService service)
    {
        _service = service;
    }

    public void PrintMonthlyReport(List<Wallet> wallets, int year, int month)
    {
        Console.WriteLine($"\nРезультаты за {year}-{month:D2}\n");
        foreach (var wallet in wallets)
        {
            PrintWalletSummary(wallet, year, month);
        }
    }

    public void PrintWalletBalances(string walletName, string walletCurrency,
        decimal initialBalance, decimal currentBalance)
    {
        Console.WriteLine($"=== {walletName} ({walletCurrency}) ===" +
                          $"\nНачальный баланс: {initialBalance:N2}" +
                          $"\nТекущий баланс: {currentBalance:N2}");
    }
    
    private void PrintWalletSummary(Wallet wallet, int year, int month)
    {
        PrintWalletBalances(wallet.Name, wallet.Currency, wallet.InitialBalance, wallet.CurrentBalance);

        // Получаем группы и их суммы
        var grouped = _service.GetGroupedTransactionsForWallet(wallet, year, month);
        var monthlySums = _service.GetMonthlySums(wallet, year, month);

        // Сортируем группы по общей сумме по убыванию
        var sortedGroups = monthlySums
            .OrderByDescending(kv => kv.Value)
            .Select(kv => kv.Key)
            .ToList();

        foreach (var groupType in sortedGroups)
        {
            PrintGroupSummary(groupType, monthlySums[groupType], grouped[groupType]);
        }
        
        var topExpenses = _service.GetTopExpensesPerWallet(year, month)
            .First(pair => pair.Wallet == wallet).TopExpenses;
        PrintTopExpensesPerWallet(topExpenses);
    }
    
    private void PrintTopExpensesPerWallet(List<Transaction> topExpenses)
    {
        Console.WriteLine("\n3 самых больших траты:");
        if (topExpenses.Any())
        {
            foreach (var transaction in topExpenses)
            {
                Console.WriteLine($"  {transaction.Date:dd-MM-yyyy} {transaction.Amount:N2} — {transaction.Description}");
            }
        }
        else
        {
            Console.WriteLine("  (нет трат)");
        }
        Console.WriteLine("\n");
    }
    
    private void PrintGroupSummary(TransactionType type, decimal sum, List<Transaction> transactions)
    {
        var typeName = type == TransactionType.Income ? "Пополнения" : "Расходы";

        Console.WriteLine($"\n-- {typeName} (сумма {sum:N2}) --");

        if (transactions is { Count: > 0 })
        {
            foreach (var transaction in transactions.OrderBy(t => t.Date))
            {
                Console.WriteLine(transaction.ToString());
            }
        }
        else
        {
            Console.WriteLine("  (нет транзакций)");
        }
    }
}
