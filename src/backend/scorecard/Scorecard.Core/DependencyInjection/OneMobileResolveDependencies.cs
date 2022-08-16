using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace Scorecard.Core.DependencyInjection
{

    public class OneMobileResolveDependencies : IResolveDependencies
    {
        ILifetimeScope _lifetimeScope;
        private string Tenant
        {
            get { return "OneMobileResolveDependencies"; }
        }

        public OneMobileResolveDependencies(ILifetimeScope lifetimeScope)
        {
            _lifetimeScope = lifetimeScope;
        }

        public bool IsRegistered<T>()
        {
            return _lifetimeScope.IsRegistered<T>();
        }

        public bool IsRegistered<T>(string name)
        {
            return _lifetimeScope.IsRegisteredWithName<T>(name);
        }

        public bool IsRegistered(Type type)
        {
            return _lifetimeScope.IsRegistered(type);

        }

        public bool IsRegistered(Type type, string name)
        {
            return _lifetimeScope.IsRegisteredWithName(name, type);

        }

        public T Resolve<T>()
        {
            if (IsRegistered<T>(Tenant))
                return Resolve<T>(Tenant);
            return _lifetimeScope.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _lifetimeScope.ResolveNamed<T>(name);
        }

        public object Resolve(Type type)
        {
            if (IsRegistered(type, Tenant))
                return Resolve(type, Tenant);

            return _lifetimeScope.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return _lifetimeScope.ResolveNamed(name, type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            if (IsRegistered<T>(Tenant))
                return Resolve<IEnumerable<T>>(Tenant);

            return _lifetimeScope.Resolve<IEnumerable<T>>();
        }

        public T ResolveOptional<T>() where T : class
        {
            if (IsRegistered<T>(Tenant))
                return ResolveOptional<T>(Tenant);

            return _lifetimeScope.ResolveOptional<T>();
        }

        public T ResolveOptional<T>(string name) where T : class
        {
            return _lifetimeScope.ResolveOptionalNamed<T>(name);
        }

        public object ResolveOptional(Type type)
        {
            if (IsRegistered(type, Tenant))
                return ResolveOptional(type, Tenant);
            return _lifetimeScope.ResolveOptional(type);

        }

        public object ResolveOptional(Type type, string name)
        {
            if (_lifetimeScope.TryResolveNamed(name, type, out object instance))
                return instance;
            return null;

        }
    }

}
