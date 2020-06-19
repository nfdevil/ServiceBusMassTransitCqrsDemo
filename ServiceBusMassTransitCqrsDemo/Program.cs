using System;
using System.Linq;
using System.Threading.Tasks;

using Autofac;

using CSharpFunctionalExtensions;

using MassTransit;

using MediatR;

using ServiceBusMassTransitCqrsDemo.Domain;

using SharedKernel;
using SharedKernel.Framework;
using SharedKernel.Framework.Functional;

namespace ServiceBusMassTransitCqrsDemo
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<ChatHub>();
            builder.AddMassTransit(x =>
            {
                x.AddBus(context => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    cfg.Host(new Uri(MassTransitConfig.HostUrl),
                             host =>
                             {
                                 host.Username("root");
                                 host.Password("oirhcD9Ahi1Jjck64NubSbTERfFG1ZQYqvosZi6USf45zwZUnefgTHP9CQpT3C9LCyBxC53R9GkkL");
                             });
                    // // Configure all consumers
                    //cfg.ReceiveEndpoint(MassTransitConfig.EndPoint, x => x.ConfigureConsumers(context));
                    // Configure the endpoints by convention
                    //cfg.ReceiveEndpoint(ep => ep.);
                }));
            });
            builder.AddMediatR(typeof(Program).Assembly);


            IContainer container = builder.Build();

            var busControl = container.Resolve<IBusControl>();
            Console.WriteLine("Starting bus ...");
            // Important! The bus must be started before using it!
            await busControl.StartAsync();
            var chatHub = container.Resolve<ChatHub>();

            try
            {
                await chatHub.Run();
            }
            finally
            {
                await busControl.StopAsync();
                Console.WriteLine("Bus stopped!");
            }
        }
    }

    public class ChatHub
    {
        private readonly IMediator _mediator;

        public ChatHub(IMediator mediator)
        {
            _mediator = mediator;
        }
        public async Task Run()
        {
            Console.WriteLine("Chathub started");
            do
            {
                string value = await Task.Run(() =>
                {
                    Console.WriteLine("Enter message (or exit to exit)");
                    Console.Write("> ");
                    return Console.ReadLine();
                });

                if ("exit".Equals(value, StringComparison.OrdinalIgnoreCase))
                    break;
                Result result = await _mediator.Send(new SendChatMessage(value));
                if (result.IsFailure)
                {
                    foreach (string errorMessage in result.GetErrorMessages())
                    {
                        Console.WriteLine($"ERROR: {errorMessage}");
                    }
                   
                }
            }
            while (true);

        }
    }
}
