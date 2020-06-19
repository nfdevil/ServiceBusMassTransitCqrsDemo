using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CSharpFunctionalExtensions;

using MediatR;

namespace SharedKernel.Framework
{
    public interface ICommand : IRequest<Result> { }
}
