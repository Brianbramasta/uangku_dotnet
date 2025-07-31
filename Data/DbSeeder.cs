using MySimpleApi.Models;

namespace MySimpleApi.Data
{
    public static class DbSeeder
    {
        public static async Task SeedData(IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();
            var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

            // Seed Users
            if (!context.Users.Any())
            {
                var user = new User
                {
                    Name = "Brian",
                    Email = "brian@mail.com",
                    Password = BCrypt.Net.BCrypt.HashPassword("password123")
                };
                context.Users.Add(user);
                await context.SaveChangesAsync();

                // Seed Categories
                var categories = new List<Category>
                {
                    new Category { UserId = user.Id, Name = "Gaji", Type = "income", Icon = "💼" },
                    new Category { UserId = user.Id, Name = "Investasi", Type = "income", Icon = "📈" },
                    new Category { UserId = user.Id, Name = "Makan", Type = "expense", Icon = "🍽️" },
                    new Category { UserId = user.Id, Name = "Transport", Type = "expense", Icon = "🚗" },
                    new Category { UserId = user.Id, Name = "Belanja", Type = "expense", Icon = "🛒" },
                    new Category { UserId = user.Id, Name = "Hiburan", Type = "expense", Icon = "🎮" }
                };
                context.Categories.AddRange(categories);
                await context.SaveChangesAsync();

                // Seed Transactions
                var transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        UserId = user.Id,
                        CategoryId = categories[0].Id,
                        Amount = 5000000,
                        Type = "income",
                        Date = DateTime.Now.Date,
                        Note = "Gaji Bulan Juli"
                    },
                    new Transaction
                    {
                        UserId = user.Id,
                        CategoryId = categories[2].Id,
                        Amount = 45000,
                        Type = "expense",
                        Date = DateTime.Now.Date,
                        Note = "Sarapan"
                    },
                    new Transaction
                    {
                        UserId = user.Id,
                        CategoryId = categories[3].Id,
                        Amount = 100000,
                        Type = "expense",
                        Date = DateTime.Now.Date,
                        Note = "Bensin"
                    }
                };
                context.Transactions.AddRange(transactions);
                await context.SaveChangesAsync();

                // Seed Budgets
                var budgets = new List<Budget>
                {
                    new Budget
                    {
                        UserId = user.Id,
                        CategoryId = categories[2].Id, // Makan
                        AmountLimit = 1000000,
                        Period = "monthly",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
                    },
                    new Budget
                    {
                        UserId = user.Id,
                        CategoryId = categories[3].Id, // Transport
                        AmountLimit = 500000,
                        Period = "monthly",
                        StartDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1),
                        EndDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1).AddMonths(1).AddDays(-1)
                    }
                };
                context.Budgets.AddRange(budgets);
                await context.SaveChangesAsync();
            }
        }
    }
}
