using System.Threading.Tasks;
using Game.Remoting.Contracts.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Game.Remoting.Client.Extensions;

public static class ServiceCollectionExtensions
{
    public static async Task<IServiceCollection> AddRemoteServices(this IServiceCollection services)
    {
        var client = new Client();
        await client.Connect();
        services.AddSingleton(client);
        services.AddSingleton(client.GetService<ITestService>());
        
        return services;
    }
}