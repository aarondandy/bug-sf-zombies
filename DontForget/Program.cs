using System;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using RemindMe.Interfaces;

namespace DontForget
{
    class Program
    {
        static void Main(string[] args)
        {
            // use this to start the reproduction of the issue
            var proxyFactory = new ActorProxyFactory();
            var proxy = proxyFactory.CreateActorProxy<IRemindMe>(new Uri("fabric:/RedundantReminderRepro/RemindMeActorService"), new ActorId(1337));
            proxy.CreateReminderAsync("get toilet paper from store").Wait();
        }
    }
}
