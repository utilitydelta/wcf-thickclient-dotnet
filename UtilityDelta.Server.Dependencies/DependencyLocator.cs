using Autofac;
using UtilityDelta.Server.Domain;

namespace UtilityDelta.Server.Dependencies
{
    public class DependencyLocator : IDependencyLocator
    {
        private readonly ILifetimeScope m_scope;

        public DependencyLocator()
        {
            var builder = new ContainerBuilder();

            //Register types
            builder.RegisterAssemblyTypes(typeof(TestService).Assembly)
                .AsImplementedInterfaces()
                .SingleInstance();

            m_scope = builder.Build().BeginLifetimeScope();
        }

        public void Dispose() => m_scope?.Dispose();

        public T GetService<T>() => m_scope.Resolve<T>();
    }
}