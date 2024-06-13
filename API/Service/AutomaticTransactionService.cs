using API.Data;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Service
{
    public class AutomaticTransactionService : BackgroundService
    {
        private readonly IServiceProvider _serviceProvider;

    public AutomaticTransactionService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        while (!stoppingToken.IsCancellationRequested)
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

                var now = DateTime.UtcNow;
                var scheduledTransactions = await context.AutomaticTransactions
                    .Where(st => st.NextExecutionDate <= now)
                    .ToListAsync();

                foreach (var scheduledTransaction in scheduledTransactions)
                {
                    var transaction = new Transaction
                    {
                        TransactionAmount = scheduledTransaction.TransactionAmount,
                        TransactionType = scheduledTransaction.TransactionType,
                        TransactionDescription = scheduledTransaction.TransactionDescription,
                        TransactionDate = scheduledTransaction.TransactionDate,
                        AppUserId = scheduledTransaction.AppUserId,
                        InsertedDate = DateTime.UtcNow
                    };

                    context.Transactions.Add(transaction);

                    switch (scheduledTransaction.Frequency)
                    {
                        case FrequencyType.Weekly:
                            scheduledTransaction.NextExecutionDate = scheduledTransaction.NextExecutionDate.AddDays(7);
                            break;
                        case FrequencyType.Monthly:
                            scheduledTransaction.NextExecutionDate = scheduledTransaction.NextExecutionDate.AddMonths(1);
                            break;
                        case FrequencyType.Yearly:
                            scheduledTransaction.NextExecutionDate = scheduledTransaction.NextExecutionDate.AddYears(1);
                            break;
                    }

                    context.AutomaticTransactions.Update(scheduledTransaction);
                }

                await context.SaveChangesAsync();
            }

            await Task.Delay(TimeSpan.FromHours(1), stoppingToken); // Check every hour
        }
    }
    }
}