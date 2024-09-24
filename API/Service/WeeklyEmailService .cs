using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Interface;
using Microsoft.AspNetCore.Identity;

namespace API.Service
{
    public class WeeklyEmailService : IHostedService, IDisposable
    {
        private readonly IServiceScopeFactory _scopeFactory;
        private Timer _timer;

        public WeeklyEmailService(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }
        public Task StartAsync(CancellationToken cancellationToken)
        {
            // Schedule the service to run at the start of every week (Monday at 12:00 AM)
            var nextRunTime = GetNextMondayAtMidnight();
            var delay = nextRunTime - DateTime.UtcNow;
            _timer = new Timer(SendWeeklyEmails, null, delay, TimeSpan.FromDays(7));

            return Task.CompletedTask;
        }

        private void SendWeeklyEmails(object state)
        {
            using (var scope = _scopeFactory.CreateScope())
            {
                var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
                var transactionRepo = scope.ServiceProvider.GetRequiredService<ITransactionCalculationsRepository>();

                // Get all users
                var users = userManager.Users.ToList();

                // Send email for every user
                foreach (var user in users)
                {
                    transactionRepo.SendWeeklySummaryEmail(user.Id);
                }
            }
        }

        private DateTime GetNextMondayAtMidnight()
        {
            var now = DateTime.UtcNow;
            var daysUntilNextMonday = ((int)DayOfWeek.Monday - (int)now.DayOfWeek + 7) % 7;
            return now.AddDays(daysUntilNextMonday).Date;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _timer?.Dispose();
        }
    }
}