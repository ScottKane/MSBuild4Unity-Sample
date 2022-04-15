using System;
using System.Globalization;
using System.Reactive.Linq;
using Game.Remoting.Contracts.Services;
using UnityEngine;

namespace Game.Services
{
    public class TestService
    {
        private readonly ITestService _service;
        public TestService(ITestService service) => _service = service;
        
        public void Run()
        {
            var requests = Observable
                .Interval(TimeSpan.FromSeconds(1))
                .Select(_ => DateTime.UtcNow.ToString(CultureInfo.InvariantCulture));

            requests.Subscribe(request => Debug.Log($"[Client]: {request}"));
                    
            var responses = _service.Subscribe(requests);
            responses.Subscribe(response => Debug.Log($"[Server]: {response}"));
        }
    }
}