using System.ComponentModel;

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
public enum Role
{
    [Description("Admin Role")]
    Admin,
    [Description("User Role")]
    User
}
