using Domain.DTO;
using Domain.IRepository;
using Repository.Context;

namespace Repository.Repository
{
    public class UserRepository : BaseRepository<UserDTO>, IUserRepository
    {
        public UserRepository(QuickBuyContext quickBuyContext) : base(quickBuyContext)
        {

        }
    }
}