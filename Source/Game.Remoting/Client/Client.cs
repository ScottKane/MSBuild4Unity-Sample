using System;
using System.Net;
using System.Threading.Tasks;
using ProtoBuf.Grpc.Client;
using ProtoBuf.Grpc.Lite;

namespace Game.Remoting.Client;

public class Client
{
    private LiteChannel _channel;
    
    public async Task Connect() => _channel = await ConnectionFactory
        .ConnectSocket(new IPEndPoint(IPAddress.Loopback, 5000))
        .AsStream()
        .AsFrames()
        .CreateChannelAsync();

    public T GetService<T>() where T : class => _channel.CreateGrpcService<T>();
}