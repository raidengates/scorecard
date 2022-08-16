using Scorecard.Core.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Data.Models;
public class Permission : ModelBase<Guid>
{
    public Permission() : base(Guid.NewGuid())
    {

    }
    public Permission(Guid id) : base(id)
    {

    }
    public string Name { get; set; }
    public Role Role { get; set; }
}
