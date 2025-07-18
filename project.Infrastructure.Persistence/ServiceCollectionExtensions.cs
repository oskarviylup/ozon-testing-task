using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using project.Application.Abstractions;
using project.Application.Abstractions.Repositories;
using project.Infrastructure.Persistence.Repositories;

namespace project.Infrastructure.Persistence;

public static class ServiceCollectionExtensions
{
    public static void AddRepositories(this IServiceCollection collection)
    {
        collection.AddScoped(typeof(IBalanceRepository), typeof(BalanceRepository));
        collection.AddScoped(typeof(IItemViewsRepository), typeof(ItemViewsRepository));
        collection.AddScoped(typeof(IItemPaymentsRepository), typeof(ItemPaymentsRepository));
        collection.AddScoped(typeof(IReportRequestRepository), typeof(ReportRequestRepository));
        collection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
    }
}