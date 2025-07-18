using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace Presentation.Grpc;

public static class GrpcServiceCollectionExtensions
{
    public static void AddGrpc(this WebApplicationBuilder builder)
    {
        builder.Services.AddGrpc();
    }
}