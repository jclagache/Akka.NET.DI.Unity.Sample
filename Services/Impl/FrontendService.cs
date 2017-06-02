using Akka.Actor;
using Akka.NET.DI.Unity.Sample.Actors;
using Akka.NET.DI.Unity.Sample.Messages;

namespace Akka.NET.DI.Unity.Sample.Services.Impl
{
    public class FrontendServiceWithExplicitDependency : IFrontendService
    {
        private readonly IActorRef _actor;

        public FrontendServiceWithExplicitDependency(IActorRef actor)
        {
            _actor = actor;
        }

        public bool Process(ProcessMessage message)
        {
            return _actor.Ask<ProcessingMessage>(message).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    return false;
                }
                else if (t.IsCanceled)
                {
                    return false;
                }
                return true;
            }).Result;
        }
    }
    
    public class FrontendServiceWithoutExplicitDependency : IFrontendService
    {       
        
        public FrontendServiceWithoutExplicitDependency()
        {
        }
        
        public bool Process(ProcessMessage message)
        {
            return ActorSystemRefs.ParentActor.Ask<ProcessingMessage>(message).ContinueWith(t =>
            {
                if (t.IsFaulted)
                {
                    return false;
                }
                else if (t.IsCanceled)
                {
                    return false;
                }
                return true;
            }).Result;
        }
    }
}