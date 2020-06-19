using System;
using System.Threading.Tasks;

using MassTransit;

using SharedKernel.Events;

namespace ServiceBusConsumer2
{
    internal class Program
    {
        private static async Task Main(string[] args)
        {
            IBusControl busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host(new Uri("rabbitmq://localhost:5672/servicebustest"),
                         host =>
                         {
                             host.Username("root");
                             host.Password("oirhcD9Ahi1Jjck64NubSbTERfFG1ZQYqvosZi6USf45zwZUnefgTHP9CQpT3C9LCyBxC53R9GkkL");
                         });
                cfg.ReceiveEndpoint(Guid.NewGuid().ToString("N"), ec => { ec.Consumer<ChatMessageConsumer>(); });
            });
            Console.WriteLine("Starting bus ...");
            // Important! The bus must be started before using it!
            await busControl.StartAsync();

            Console.WriteLine("Press ENTER to close...");
            while (Console.ReadKey(true).Key != ConsoleKey.Enter) { }
        }
    }

    public class ChatMessageConsumer : IConsumer<ChatMessageSent>
    {
        public async Task Consume(ConsumeContext<ChatMessageSent> context)
        {
            ChatMessageSent chatMessageSentEvent = context.Message;
            await Task.Run(() => Console.WriteLine($"[{chatMessageSentEvent.PostTime:G}]{chatMessageSentEvent.UserId}: {chatMessageSentEvent.Message}"));
        }
    }
}