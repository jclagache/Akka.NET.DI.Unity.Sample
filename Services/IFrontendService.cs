using Akka.NET.DI.Unity.Sample.Messages;

namespace Akka.NET.DI.Unity.Sample.Services
{
    public interface IFrontendService
    {
        bool Process(ProcessMessage message);
    }
}