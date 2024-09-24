using System.Globalization;
using System.Text;
using API.Data;
using API.DTOs.Transaction;
using API.Interface;
using API.Models;
using Microsoft.EntityFrameworkCore;

namespace API.Repository
{
    public class TransactionCalculationsRepository : ITransactionCalculationsRepository
    {
        private readonly AppDbContext _context;
        private readonly IEmailService _emailService;

        public TransactionCalculationsRepository(AppDbContext context, IEmailService emailService)
        {
            _context = context;
            _emailService = emailService;
        }

        // Get total values (Gain, Spent, Profit) for the dashboard
        public async Task<DashboardDto> GetTotalValuesAsync(string userId)
        {
            var totalGain = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var totalSpent = await _context.Transactions
                .Where(e => e.AppUserId == userId && !e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var totalProfit = totalGain - totalSpent;

            return new DashboardDto
            {
                Profit = totalProfit,
                Gain = totalGain,
                Spent = totalSpent
            };
        }

        // Get yearly values (Gain, Spent, Profit) for the dashboard
        public async Task<DashboardDto> GetYearlyValuesAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;
            var transactions = _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear);

            var yearlyGain = await transactions
                .Where(e => e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var yearlySpent = await transactions
                .Where(e => !e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var yearlyProfit = yearlyGain - yearlySpent;

            return new DashboardDto
            {
                Profit = yearlyProfit,
                Gain = yearlyGain,
                Spent = yearlySpent
            };
        }

        // Get monthly values (Gain, Spent, Profit) for the dashboard
        public async Task<DashboardDto> GetMonthlyValuesAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;
            var currentMonth = DateTime.UtcNow.Month;
            var transactions = _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear && e.TransactionDate.Month == currentMonth);

            var monthlyGain = await transactions
                .Where(e => e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var monthlySpent = await transactions
                .Where(e => !e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var monthlyProfit = monthlyGain - monthlySpent;

            return new DashboardDto
            {
                Profit = monthlyProfit,
                Gain = monthlyGain,
                Spent = monthlySpent
            };
        }

        // Get chart data for total gains
        public async Task<IEnumerable<DashboardChartDto>> GetTotalGainChartAsync(string userId)
        {
            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionType)
                .OrderBy(e => e.TransactionDate)
                .Select(e => new DashboardChartDto
                {
                    Value = (double)e.TransactionAmount,
                    Date = e.TransactionDate
                })
                .ToListAsync();

            return transactions;
        }

        // Get chart data for total spending
        public async Task<IEnumerable<DashboardChartDto>> GetTotalSpentChartAsync(string userId)
        {
            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && !e.TransactionType)
                .OrderBy(e => e.TransactionDate)
                .Select(e => new DashboardChartDto
                {
                    Value = (double)e.TransactionAmount,
                    Date = e.TransactionDate
                })
                .ToListAsync();

            return transactions;
        }

        // Get chart data for total profit
        public async Task<IEnumerable<DashboardChartDto>> GetTotalProfitChartAsync(string userId)
        {
            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId)
                .OrderBy(e => e.TransactionDate)
                .ToListAsync();

            var cumulativeValues = new List<DashboardChartDto>();
            double cumulativeSum = 0;

            foreach (var transaction in transactions)
            {
                cumulativeSum += transaction.TransactionType ? (double)transaction.TransactionAmount : -(double)transaction.TransactionAmount;

                cumulativeValues.Add(new DashboardChartDto
                {
                    Value = cumulativeSum,
                    Date = transaction.TransactionDate
                });
            }

            return cumulativeValues;
        }

        // Get chart data for yearly gains
        public async Task<IEnumerable<DashboardChartDto>> GetYearlyGainChartAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;

            var monthlyTransactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear && e.TransactionType)
                .GroupBy(e => new { e.TransactionDate.Year, e.TransactionDate.Month })
                .Select(g => new DashboardChartDto
                {
                    Value = g.Sum(e => (double)e.TransactionAmount),
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1)
                })
                .ToListAsync();

            return monthlyTransactions;
        }

        // Get chart data for yearly spending
        public async Task<IEnumerable<DashboardChartDto>> GetYearlySpentChartAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;

            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear && !e.TransactionType)
                .GroupBy(e => new { e.TransactionDate.Year, e.TransactionDate.Month })
                .Select(g => new DashboardChartDto
                {
                    Value = g.Sum(e => (double)e.TransactionAmount),
                    Date = new DateTime(g.Key.Year, g.Key.Month, 1)
                })
                .ToListAsync();

            return transactions;
        }

        // Get chart data for yearly profit
        public async Task<IEnumerable<DashboardChartDto>> GetYearlyProfitChartAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;

            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear)
                .OrderBy(e => e.TransactionDate)
                .ToListAsync();

            var cumulativeValues = new List<DashboardChartDto>();
            double cumulativeSum = 0;

            foreach (var transaction in transactions)
            {
                cumulativeSum += transaction.TransactionType ? (double)transaction.TransactionAmount : -(double)transaction.TransactionAmount;

                cumulativeValues.Add(new DashboardChartDto
                {
                    Value = cumulativeSum,
                    Date = transaction.TransactionDate
                });
            }

            return cumulativeValues;
        }

        // Get chart data for monthly gains
        public async Task<IEnumerable<DashboardChartDto>> GetMonthlyGainChartAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;
            var currentMonth = DateTime.UtcNow.Month;

            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear && e.TransactionDate.Month == currentMonth && e.TransactionType)
                .GroupBy(e => e.TransactionDate.Date)
                .Select(g => new DashboardChartDto
                {
                    Date = g.Key,
                    Value = g.Sum(e => (double)e.TransactionAmount)
                })
                .OrderBy(e => e.Date)
                .ToListAsync();

            return transactions;
        }

        // Get chart data for monthly spending
        public async Task<IEnumerable<DashboardChartDto>> GetMonthlySpentChartAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;
            var currentMonth = DateTime.UtcNow.Month;

            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear && e.TransactionDate.Month == currentMonth && !e.TransactionType)
                .GroupBy(e => e.TransactionDate.Date)
                .Select(g => new DashboardChartDto
                {
                    Date = g.Key,
                    Value = g.Sum(e => (double)e.TransactionAmount)
                })
                .OrderBy(e => e.Date)
                .ToListAsync();

            return transactions;
        }

        // Get chart data for monthly profit
        public async Task<IEnumerable<DashboardChartDto>> GetMonthlyProfitChartAsync(string userId)
        {
            var currentYear = DateTime.UtcNow.Year;
            var currentMonth = DateTime.UtcNow.Month;

            var transactions = await _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate.Year == currentYear && e.TransactionDate.Month == currentMonth)
                .OrderBy(e => e.TransactionDate)
                .ToListAsync();

            var groupedTransactions = transactions
                .GroupBy(t => t.TransactionDate.Date)
                .Select(g => new
                {
                    Date = g.Key,
                    Sum = g.Sum(t => t.TransactionType ? (double)t.TransactionAmount : -(double)t.TransactionAmount)
                })
                .OrderBy(g => g.Date)
                .ToList();

            var cumulativeValues = new List<DashboardChartDto>();
            double cumulativeSum = 0;

            foreach (var dailyTransaction in groupedTransactions)
            {
                cumulativeSum += dailyTransaction.Sum;

                cumulativeValues.Add(new DashboardChartDto
                {
                    Value = cumulativeSum,
                    Date = dailyTransaction.Date
                });
            }

            return cumulativeValues;
        }




        public async Task SendWeeklySummaryEmail(string userId)
        {
            // Get calculations for current week, month, year, and totals
            var weeklyValues = await GetWeeklyValuesAsync(userId);
            var monthlyValues = await GetMonthlyValuesAsync(userId);
            var yearlyValues = await GetYearlyValuesAsync(userId);
            var totalValues = await GetTotalValuesAsync(userId);

            // Fetch all transactions for the current week
            var startOfWeek = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek); // Last Monday
            var transactions = await _context.Transactions
                .Where(t => t.AppUserId == userId && t.TransactionDate >= startOfWeek)
                .ToListAsync();

            // Compose email content
            var message = BuildWeeklySummaryEmail(transactions, weeklyValues, monthlyValues, yearlyValues, totalValues);

            // Fetch user email
            var user = await _context.Users.FindAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found");

            // Send the email
            await _emailService.SendEmailAsync(user.Email, "Weekly Transaction Summary", message);
        }

        private string BuildWeeklySummaryEmail(List<Transaction> transactions, DashboardDto weekly, DashboardDto monthly, DashboardDto yearly, DashboardDto total)
        {
            var emailContent = new StringBuilder();
            emailContent.AppendLine("<h1>Weekly Transaction Summary</h1>");

            emailContent.AppendLine("<h2>Transactions This Week:</h2>");
            foreach (var transaction in transactions)
            {
                emailContent.AppendLine($"<p>{transaction.TransactionDate.ToString("d", CultureInfo.InvariantCulture)}: {transaction.TransactionDescription} - {(transaction.TransactionType ? "+" : "-")}${transaction.TransactionAmount:F2}</p>");
            }

            emailContent.AppendLine("<h2>Weekly Summary:</h2>");
            emailContent.AppendLine($"<p>Gain: ${weekly.Gain:F2}</p>");
            emailContent.AppendLine($"<p>Spent: ${weekly.Spent:F2}</p>");
            emailContent.AppendLine($"<p>Profit: ${weekly.Profit:F2}</p>");

            emailContent.AppendLine("<h2>Monthly Summary:</h2>");
            emailContent.AppendLine($"<p>Gain: ${monthly.Gain:F2}</p>");
            emailContent.AppendLine($"<p>Spent: ${monthly.Spent:F2}</p>");
            emailContent.AppendLine($"<p>Profit: ${monthly.Profit:F2}</p>");

            emailContent.AppendLine("<h2>Yearly Summary:</h2>");
            emailContent.AppendLine($"<p>Gain: ${yearly.Gain:F2}</p>");
            emailContent.AppendLine($"<p>Spent: ${yearly.Spent:F2}</p>");
            emailContent.AppendLine($"<p>Profit: ${yearly.Profit:F2}</p>");

            emailContent.AppendLine("<h2>Total Summary:</h2>");
            emailContent.AppendLine($"<p>Gain: ${total.Gain:F2}</p>");
            emailContent.AppendLine($"<p>Spent: ${total.Spent:F2}</p>");
            emailContent.AppendLine($"<p>Profit: ${total.Profit:F2}</p>");

            return emailContent.ToString();
        }

        private async Task<DashboardDto> GetWeeklyValuesAsync(string userId)
        {
            var startOfWeek = DateTime.UtcNow.AddDays(-(int)DateTime.UtcNow.DayOfWeek); // Last Monday
            var transactions = _context.Transactions
                .Where(e => e.AppUserId == userId && e.TransactionDate >= startOfWeek);

            var weeklyGain = await transactions
                .Where(e => e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var weeklySpent = await transactions
                .Where(e => !e.TransactionType)
                .SumAsync(e => (double)e.TransactionAmount);

            var weeklyProfit = weeklyGain - weeklySpent;

            return new DashboardDto
            {
                Profit = weeklyProfit,
                Gain = weeklyGain,
                Spent = weeklySpent
            };
        }
    }
}
