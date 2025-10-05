using FinanceApp.Services;
using FinanceApp.Models;


namespace FinanceApp.Tests.Services;
[TestFixture]
public class FinanceServiceTests
{
    private FinanceService _service;
    private Wallet _wallet1, _wallet2;
    
    [SetUp]
    public void Setup()
    {
        // Arrange
        _wallet1 = new Wallet("Кошелёк 1", "RUB", 1000m);
        _wallet1.AddTransaction(new Transaction(new DateTime(2025, 5, 1), 500, TransactionType.Income, "Зарплата"));
        _wallet1.AddTransaction(new Transaction(new DateTime(2025, 5, 3), 200, TransactionType.Expense, "Еда"));
        _wallet1.AddTransaction(new Transaction(new DateTime(2025, 5, 5), 100, TransactionType.Expense, "Кофе"));
        _wallet1.AddTransaction(new Transaction(new DateTime(2025, 5, 10), 50, TransactionType.Expense, "Транспорт"));

        _wallet2 = new Wallet("Кошелёк 2", "USD", 500m);
        _wallet2.AddTransaction(new Transaction(new DateTime(2025, 5, 2), 300, TransactionType.Expense, "Магазин"));
        _wallet2.AddTransaction(new Transaction(new DateTime(2025, 5, 8), 700, TransactionType.Income, "Фриланс"));


        _service = new FinanceService(new List<Wallet> { _wallet1, _wallet2 });
    }
    
    [Test]
    public void GetGroupedTransactionsForWallet_ShouldGroupByTypeAndSortByDate()
    {
        // Arrange
        int year = 2025;
        int month = 5;
        
        // Act
        var grouped = _service.GetGroupedTransactionsForWallet(_wallet1, year, month);
        
        // Assert
        Assert.That(grouped.Count(), Is.LessThanOrEqualTo(2));
        Assert.That(grouped[TransactionType.Expense][0].Date <= grouped[TransactionType.Expense][grouped[TransactionType.Expense].Count - 1].Date, Is.True);
    }
    
    [Test]
    public void GetTopExpensesPerWallet_Returns_Top3ExpensesSortedByAmount()
    {
        // Arrange
        int year = 2025;
        int month = 5;
        
        // Act
        var result = _service.GetTopExpensesPerWallet(year, month);

        // Assert
        Assert.That(result, Has.Count.EqualTo(2));

        var wallet1Top = result.First(r => r.Wallet == _wallet1).TopExpenses;
        Assert.That(wallet1Top.Count, Is.EqualTo(3)); // 3 самые большие траты
        Assert.That(wallet1Top[0].Amount, Is.GreaterThanOrEqualTo(wallet1Top[1].Amount));
        Assert.That(wallet1Top[1].Amount, Is.GreaterThanOrEqualTo(wallet1Top[2].Amount));
        
        Assert.That(wallet1Top.All(t => t.Type == TransactionType.Expense), Is.True);
    }
    
    [Test]
    public void GetMonthlySums_ReturnsZero_WhenNoTransactions()
    {
        // Arrange
        var emptyWallet = new Wallet("Пустой", "RUB", 0m);

        // Act
        var sums = _service.GetMonthlySums(emptyWallet, 2025, 5);

        // Assert
        Assert.That(sums[TransactionType.Income], Is.EqualTo(0));
        Assert.That(sums[TransactionType.Expense], Is.EqualTo(0));
    }
}