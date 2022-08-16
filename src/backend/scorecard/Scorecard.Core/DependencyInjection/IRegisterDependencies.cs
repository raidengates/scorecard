using Scorecard.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.DependencyInjection
{
    public interface IRegisterDependencies
    {
        void Register(Type type, params DependencyParameter[] parameters);

        void Register<TFrom, TTo>(params DependencyParameter[] parameters) where TTo : TFrom;

        void RegisterNamed<TFrom, TTo>(string name, LifeTimeScope scope, params DependencyParameter[] parameters) where TTo : TFrom;

        void RegisterSingleton<TFrom, TTo>(params DependencyParameter[] parameters) where TTo : TFrom;

        void RegisterSingletonNamed<TFrom, TTo>(string name, params DependencyParameter[] parameters) where TTo : TFrom;

        void RegisterInstance<TInterface>(TInterface instance) where TInterface : class;

        void RegisterInstanceNamed<TInterface>(TInterface instance, string name) where TInterface : class;

        void RegisterHub<TType>();
    }

}
