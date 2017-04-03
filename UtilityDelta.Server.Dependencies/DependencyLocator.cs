using System;
using Autofac;
using UtilityDelta.Server.Domain;
using UtilityDelta.Shared.Common;

namespace UtilityDelta.Server.Dependencies
{
    public class DependencyLocator : IDependencyLocator
    {
        private readonly ILifetimeScope m_scope;

        public DependencyLocator()
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule(new LoggingModule());
            builder.RegisterType<Serializer>()
                .AsImplementedInterfaces()
                .SingleInstance();
            builder.RegisterAssemblyTypes(typeof(TestService).Assembly)
                .AsImplementedInterfaces()
                .SingleInstance();

            m_scope = builder.Build().BeginLifetimeScope();
        }

        public void Dispose() => m_scope?.Dispose();

        public T GetService<T>() => m_scope.Resolve<T>();

        public object GetService(Type type) => m_scope.Resolve(type);
    }
}