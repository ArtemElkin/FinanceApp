# FinanceApp — консольное приложение для учёта личных финансов
Простое .NET-приложение, реализующее кошельки и транзакции с подсчётом баланса, ограничением расходов и базовыми отчётами.
## Сущности
- Кошелёк
- Транзакция

## Тестирование
Проект покрыт unit-тестами (NUnit).

## Структура

```plaintext
FinanceApp/
 ├─ Models/          # Wallet, Transaction
 ├─ Services/        # FinanceService, ReportPrinter
 ├─ Utils/           # SeedData
 └─ Program.cs
FinanceApp.Tests/    # Unit-тесты
