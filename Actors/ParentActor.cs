using Akka.Actor;
using Akka.DI.Core;
using Akka.NET.DI.Unity.Sample.Messages;
using Akka.Routing;

namespace Akka.NET.DI.Unity.Sample.Actors
{
    public class ParentActor: ReceiveActor
    {       
        private IActorRef Router { get; set; }
        
        protected override void PreStart()
        {
            Router = Context.ActorOf(Context.DI().Props<ChildActor>().WithRouter(new RoundRobinPool(2)), "childActors");            
        }

        public ParentActor()
        {
            Receive<ProcessMessage>(message =>
            {
                Sender.Tell(new ProcessingMessage());
                Router.Tell(new ProcessMessage());
            });
            Receive<ProcessedMessage>(message =>
            {
                //ok, cool !
            });
        }
    }
}