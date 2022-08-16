using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Core.DependencyInjection
{
    public class DependencyParameter
    {
        public string Key { get; set; }

        public object Value { get; set; }

        public DependencyParameter(string key, object value)
        {
            Key = key;
            Value = value;
        }
    }

}
