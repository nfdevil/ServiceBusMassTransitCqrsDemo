using System;
using System.Threading;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using MassTransit;

using MediatR;

using ServiceBusMassTransitCqrsDemo.Domain;
using ServiceBusMassTransitCqrsDemo.Domain.Chat;

using SharedKernel;
using SharedKernel.Events;
using SharedKernel.Framework;
using SharedKernel.Framework.Validation;

using Message = ServiceBusMassTransitCqrsDemo.Domain.Chat.DomainEvents.Message;

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

        public async Task<Result<Unit, ValidationFailures>> Handle(SendChatMessage request, CancellationToken cancellationToken)
        {
            ISendEndpoint sendEndpoint = await _sendEndpointProvider.GetSendEndpoint(new Uri($"{MassTransitConfig.HostUrl}/{MassTransitConfig.EndPoint}"));
            // await sendEndpoint.Send(new ChatMessage("Nils", value));
            _currentUser.SendChatMessage(new Message(request.Message));
            return Result.Success<Unit, ValidationFailures>(Unit.Value);
        }
    }
}