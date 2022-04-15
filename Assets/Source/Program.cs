using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Game.Remoting.Client.Extensions;
using Game.Services;
using UnityEngine;

namespace Game
{
    internal static class Program
    {
        [RuntimeInitializeOnLoadMethod]
        public static async Task Main() =>
            await Host.CreateDefaultBuilder()
                .ConfigureServices(async services =>
                {
                    await services.AddRemoteServices();

                    services.AddSingleton<TestService>();
                    
                    services.BuildServiceProvider().GetRequiredService<TestService>().Run();
                })
                .Build()
                .RunAsync();
    }
}
