using FluentValidation;

using SharedKernel.Framework;

namespace ServiceBusMassTransitCqrsDemo.Domain
{
    public class SendChatMessage : ICommand
    {
        public SendChatMessage(string message)
        {
            Message = message;
        }

        public string Message { get; }
    }

    public class SendChatMessageValidation : AbstractValidator<SendChatMessage>
    {
        public SendChatMessageValidation()
        {
            RuleFor(x => x.Message).MinimumLength(5);
        }
    }
}