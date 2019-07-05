using Domain.DTO;
using Domain.IRepository;
using Repository.Context;

namespace Repository.Repository
{
    public class OrderItemRepository : BaseRepository<OrderItemDTO>, IOrderItemRepository
    {
        public OrderItemRepository(QuickBuyContext quickBuyContext) : base(quickBuyContext)
        {

        }
    }
}