using Akka.Actor;
using Akka.DI.Core;
using Akka.DI.Unity;
using Akka.NET.DI.Unity.Sample.Actors;
using Akka.NET.DI.Unity.Sample.Messages;
using Akka.NET.DI.Unity.Sample.Services;
using Akka.NET.DI.Unity.Sample.Services.Impl;
using Microsoft.Practices.Unity;
using NUnit.Framework;

namespace Akka.NET.DI.Unity.Sample
{
    [TestFixture]
    public class Tests
    {
              
        
        [SetUp]
        protected void SetUp()
        {
            var actorSystem = ActorSystemRefs.ActorSystem = ActorSystem.Create("SampleActorSystem");
            ActorSystemRefs.ParentActor = actorSystem.ActorOf(Props.Create<ParentActor>(), "parent"); 
        }

        [TearDown]
        public void Cleanup()
        {
            ActorSystemRefs.ActorSystem.Terminate();
        }
        
        [Test]
        public void TestFrontendServiceWithExplicitDependency()
        {
            var container = new UnityContainer();
            container.RegisterType<IBackendService, BackendService>();
            container.RegisterType<ChildActor>();

            // Create the dependency resolver
            IDependencyResolver resolver = new UnityDependencyResolver(container, ActorSystemRefs.ActorSystem);
            container.RegisterType<IFrontendService>(new InjectionFactory(c =>
            {
                return new FrontendServiceWithExplicitDependency(ActorSystemRefs.ParentActor);
            }));
            var frontEndService = container.Resolve<IFrontendService>();
            frontEndService.Process(new ProcessMessage());
                      
        }

        [Test]
        public void TestFrontendServiceWithoutExplicitDependency()
        {
            var container = new UnityContainer();
            container.RegisterType<IBackendService, BackendService>();
            container.RegisterType<ChildActor>();
            // Create the dependency resolver
            IDependencyResolver resolver = new UnityDependencyResolver(container, ActorSystemRefs.ActorSystem);
            container.RegisterType<IFrontendService, FrontendServiceWithoutExplicitDependency>();
            var frontEndService = container.Resolve<IFrontendService>();
            frontEndService.Process(new ProcessMessage());
        }


    }
}