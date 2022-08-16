using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.DependencyInjection
{
    public static class DependencyResolver
    {
        public static IResolveDependencies Instance { get; private set; }

        public static void SetResolver(IResolveDependencies resolver)
        {
            Instance = resolver;
        }
    }

}
