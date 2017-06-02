using Akka.NET.DI.Unity.Sample.Messages;

namespace Akka.NET.DI.Unity.Sample.Services
{
    public interface IBackendService
    {
        ProcessedMessage Process(ProcessMessage message);
    }
}