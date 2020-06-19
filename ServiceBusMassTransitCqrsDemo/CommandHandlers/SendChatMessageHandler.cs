using System;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using MassTransit;

using ServiceBusMassTransitCqrsDemo.Domain;

using SharedKernel;
using SharedKernel.Events;
using SharedKernel.Framework;

namespace ServiceBusMassTransitCqrsDemo.CommandHandlers
{
    public class SendChatMessageHandler : ICommandHandler<SendChatMessage>
    {
        private readonly ISendEndpointProvider _sendEndpointProvider;
        private readonly IBus _bus;
        private User _currentUser;

        public SendChatMessageHandler(ISendEndpointProvider sendEndpointProvider, IBus bus)
        {
            _sendEndpointProvider = sendEndpointProvider;
            _bus = bus;
            _currentUser = new User("nils@appsum.com");
        }

        public async Task<Result> Handle(SendChatMessage request, CancellationToken cancellationToken)
        {
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"{MassTransitConfig.HostUrl}/{MassTransitConfig.EndPoint}"));
            // await sendEndpoint.Send(new ChatMessage("Nils", value));
            var chatMessage = new ChatMessage(_currentUser, request.Message);
            await _bus.Publish(new ChatMessageSent(chatMessage.User.Id, chatMessage.Message, chatMessage.Created), cancellationToken);
            return Result.Success();
        }
    }
}