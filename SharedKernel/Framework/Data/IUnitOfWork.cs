﻿using System.Threading;
using System.Threading.Tasks;

namespace SharedKernel.Framework.Data
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync(CancellationToken cancellationToken = default);
    }
}