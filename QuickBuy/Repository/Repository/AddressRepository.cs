using Domain.DTO;
using Domain.IRepository;
using Repository.Context;

namespace Repository.Repository
{
    public class AddressRepository : BaseRepository<AddressDTO>, IAddressRepository
    {
        public AddressRepository(QuickBuyContext quickBuyContext) : base(quickBuyContext)
        {

        }
    }
}