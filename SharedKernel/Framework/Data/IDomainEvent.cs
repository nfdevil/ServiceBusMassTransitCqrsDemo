using System;

using MediatR;

namespace SharedKernel.Framework.Data
{
    public interface IDomainEvent : INotification
    {
        DateTime OccuredOn { get; }
    }
}