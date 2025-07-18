using Microsoft.EntityFrameworkCore;
using project.Application.Models.DbModels;

namespace project.Application;

public class ApplicationContext : DbContext
{
    public DbSet<ReportRequest> ReportsRequests => Set<ReportRequest>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Balance> Balances => Set<Balance>();
    public DbSet<Item> Items => Set<Item>();
    public DbSet<ItemPayment> ItemPayments => Set<ItemPayment>();
    public DbSet<ItemView> ItemViews => Set<ItemView>();
    
    public ApplicationContext(DbContextOptions<ApplicationContext> options)
        : base(options)
    {
    }
}