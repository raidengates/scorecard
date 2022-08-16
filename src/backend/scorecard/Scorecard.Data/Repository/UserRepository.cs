using Scorecard.Data.Interfaces;
using Scorecard.Data.Models;

namespace Scorecard.Data.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(IMongoContext context) : base(context)
        {
        }
    }
}
