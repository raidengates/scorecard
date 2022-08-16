using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Data.Models;
public class ModelBase<T>
{
    public ModelBase(T id)
    {
        Id = id;
    }
    public T Id { get; private set; }

    public string CreatedBy { get; set; }
    public DateTime CreatedDateTime { get; set; }
    public string LastUpdatedBy { get; set; }
    public DateTime LastUpdatedDateTime { get; set; }
}
