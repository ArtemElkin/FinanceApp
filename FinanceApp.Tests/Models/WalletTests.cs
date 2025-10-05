using FinanceApp.Models;

namespace FinanceApp.Tests.Models;

[TestFixture]
public class WalletTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void AddingExpense_ShouldDecreaseBalance()
    {
        // Arrange
        var wallet = new Wallet("test wallet", "USD", 1000m);
        var transaction = new Transaction(DateTime.Now, 500m, TransactionType.Expense, "Dantist");
        
        // Act
        wallet.AddTransaction(transaction);
        // Assert
        Assert.That(wallet.CurrentBalance, Is.EqualTo(500m));
    }
    [Test]
    public void AddingIncome_ShouldIncreaseBalance()
    {
        // Arrange
        var wallet = new Wallet("test wallet", "USD", 1500m);
        var transaction = new Transaction(DateTime.Now, 500m, TransactionType.Income, "Payment");
        
        // Act
        wallet.AddTransaction(transaction);
        // Assert
        Assert.That(wallet.CurrentBalance, Is.EqualTo(2000m));
    }
}