using System;
using System.Diagnostics;
using System.Text;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using RemindMe.Interfaces;

namespace RemindMe
{
    [StatePersistence(StatePersistence.Persisted)]
    internal class RemindMe : Actor, IRemindMe, IRemindable
    {
        public RemindMe(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        public async Task CreateReminderAsync(string text)
        {
            Debug.WriteLine($"{nameof(CreateReminderAsync)}( {text} )");

            // register expecting it to fire once
            await this.RegisterReminderAsync(
                reminderName: "ToDo",
                state: Encoding.UTF8.GetBytes(text),
                dueTime: TimeSpan.FromSeconds(1),
                period: new TimeSpan(0, 0, 0, 0, -1) // magic number to not retry the reminder
                );
        }

        public Task ReceiveReminderAsync(string reminderName, byte[] context, TimeSpan dueTime, TimeSpan period)
        {
            // expect to handle each reminder only once
            if (reminderName == "ToDo")
            {
                var text = Encoding.UTF8.GetString(context);
                Debug.WriteLine($"{nameof(ReceiveReminderAsync)}( {reminderName} , {text} , {dueTime} , {period} )");
            }
            else
            {
                Debug.WriteLine("unexpected: " + reminderName);
            }

            return Task.FromResult(0);
        }
    }
}
