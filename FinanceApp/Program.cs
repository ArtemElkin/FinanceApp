using FinanceApp.Services;
using FinanceApp.Utils;

namespace FinanceApp;

internal static class Program
{
    private static void Main()
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("Finance App\n");

        var wallets = SeedData.CreateSampleWallets();
        var service = new FinanceService(wallets);
        
        Console.Write("Введите год: ");
        string? yearInput = Console.ReadLine();
        int year;

        if (!int.TryParse(yearInput, out year))
        {
            year = DateTime.Now.Year;
        }

        Console.Write("Введите месяц (1-12): ");
        string? monthInput = Console.ReadLine();
        int month;

        if (!int.TryParse(monthInput, out month) || month < 1 || month > 12)
        {
            month = DateTime.Now.Month;
        }

        ReportPrinter printer = new ReportPrinter(service);
        var allWallets = service.GetAllWallets();
        printer.PrintMonthlyReport(allWallets, year, month);

        Console.WriteLine("Нажмите любую клавишу для завершения работы.");
        Console.ReadKey();
    }
}