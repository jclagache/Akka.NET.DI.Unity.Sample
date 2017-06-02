using Akka.Actor;
using Akka.NET.DI.Unity.Sample.Messages;
using Akka.NET.DI.Unity.Sample.Services;

namespace Akka.NET.DI.Unity.Sample.Actors
{
    public class ChildActor: ReceiveActor
    {
        private IBackendService _backendService;

        public ChildActor(IBackendService backendService)
        {
            _backendService = backendService;
            Receive<ProcessMessage>(message =>
            {
                Sender.Tell(_backendService.Process(message));
            });
        }
        
    }
}