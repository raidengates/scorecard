using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scorecard.Data.Models;
public class User : ModelBase<Guid>
{
    public User() : base(Guid.NewGuid())
    {
    }
    public User(Guid id) : base(id)
    {
    }

    public string Username { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string ResetToken { get; set; }
    public List<Permission> Permissions { get; set; }
}
