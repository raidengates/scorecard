using Autofac;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.DependencyInjection
{
    public interface ILifetimeResolveDependencies : IResolveDependencies
    {

    }
    public class LifetimeResolveDependencies : ILifetimeResolveDependencies
    {
        private readonly ILifetimeScope _scope;
        public LifetimeResolveDependencies(ILifetimeScope scope)
        {
            _scope = scope;
        }
        public bool IsRegistered<T>()
        {
            return _scope.IsRegistered<T>();
        }

        public bool IsRegistered<T>(string name)
        {
            return _scope.IsRegisteredWithName<T>(name);
        }

        public bool IsRegistered(Type type)
        {
            return _scope.IsRegistered(type);
        }

        public bool IsRegistered(Type type, string name)
        {
            return _scope.IsRegisteredWithName(name, type);
        }

        public T Resolve<T>()
        {
            return _scope.Resolve<T>();
        }

        public T Resolve<T>(string name)
        {
            return _scope.ResolveNamed<T>(name);
        }

        public object Resolve(Type type)
        {
            return _scope.Resolve(type);
        }

        public object Resolve(Type type, string name)
        {
            return _scope.ResolveNamed(name, type);
        }

        public IEnumerable<T> ResolveAll<T>()
        {
            return _scope.Resolve<IEnumerable<T>>();
        }

        public T ResolveOptional<T>() where T : class
        {
            return _scope.ResolveOptional<T>();
        }

        public T ResolveOptional<T>(string name) where T : class
        {
            return _scope.ResolveOptionalNamed<T>(name);
        }

        public object ResolveOptional(Type type)
        {
            return _scope.ResolveOptional(type);
        }

        public object ResolveOptional(Type type, string name)
        {
            object instance;
            if (this._scope.TryResolveNamed(name, type, out instance))
                return instance;
            return (object)null;
        }
    }
}
