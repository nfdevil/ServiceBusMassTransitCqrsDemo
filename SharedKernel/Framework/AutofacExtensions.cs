using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

using Autofac;

using FluentValidation;

using MediatR;

using SharedKernel.Framework.Validation;

namespace SharedKernel.Framework
{
    public static class AutofacExtensions
    {
        public static ContainerBuilder AddMediatR(this ContainerBuilder builder, params Assembly[] assemblies)
        {
            Assembly[] assembliesToScan = assemblies.Concat(new[] {typeof(AutofacExtensions).Assembly}).ToArray();

            // Mediator itself
            builder
                .RegisterType<Mediator>()
                .As<IMediator>()
                .InstancePerLifetimeScope();

            // request & notification handlers
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });

            // finally register our custom code (individually, or via assembly scanning)
            // - requests & handlers as transient, i.e. InstancePerDependency()
            // - pre/post-processors as scoped/per-request, i.e. InstancePerLifetimeScope()
            // - behaviors as transient, i.e. InstancePerDependency()
            builder.RegisterAssemblyTypes(assembliesToScan)
                   .AsClosedTypesOf(typeof(IRequestHandler<,>)).AsImplementedInterfaces().InstancePerDependency();
            builder.RegisterAssemblyTypes(assembliesToScan)
                   .AsClosedTypesOf(typeof(IQueryHandler<,>)).AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterGeneric(typeof(ValidatorPipelineBehavior<>)).AsImplementedInterfaces().InstancePerDependency();

            builder.RegisterAssemblyTypes(assembliesToScan)
                   .AsClosedTypesOf(typeof(IValidator<>)).AsImplementedInterfaces().InstancePerLifetimeScope();
            return builder;
        }

    }
}
