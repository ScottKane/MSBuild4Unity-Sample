using System;
using System.ServiceModel;

namespace Game.Remoting.Contracts.Services;

[ServiceContract]
public interface ITestService
{
    [OperationContract] string Echo(string message);
    [OperationContract] IObservable<string> Subscribe(IObservable<string> requests);
}