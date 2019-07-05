using Domain.DTO;
using Domain.IRepository;
using Repository.Context;

namespace Repository.Repository
{
    public class OrderRepository : BaseRepository<OrderDTO>, IOrderRepository
    {
        public OrderRepository(QuickBuyContext quickBuyContext) : base(quickBuyContext)
        {

        }
    }
}