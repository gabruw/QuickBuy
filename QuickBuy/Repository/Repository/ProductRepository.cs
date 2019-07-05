using Domain.DTO;
using Domain.IRepository;
using Repository.Context;

namespace Repository.Repository
{
    public class ProductRepository : BaseRepository<ProductDTO>, IProductRepository
    {
        public ProductRepository(QuickBuyContext quickBuyContext) : base(quickBuyContext)
        {

        }
    }
}