using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.DependencyInjection
{
    public interface IResolveDependencies
    {
        T Resolve<T>();

        T Resolve<T>(string name);

        T ResolveOptional<T>() where T : class;

        T ResolveOptional<T>(string name) where T : class;

        object ResolveOptional(Type type);

        object ResolveOptional(Type type, string name);

        IEnumerable<T> ResolveAll<T>();

        object Resolve(Type type);

        object Resolve(Type type, string name);

        bool IsRegistered<T>();

        bool IsRegistered<T>(string name);

        bool IsRegistered(Type type);

        bool IsRegistered(Type type, string name);
    }

}
