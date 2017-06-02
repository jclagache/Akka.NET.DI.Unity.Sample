using Akka.NET.DI.Unity.Sample.Messages;

namespace Akka.NET.DI.Unity.Sample.Services.Impl
{
    public class BackendService : IBackendService
    {
        public ProcessedMessage Process(ProcessMessage message)
        {
            return new ProcessedMessage();
        }
    }
}