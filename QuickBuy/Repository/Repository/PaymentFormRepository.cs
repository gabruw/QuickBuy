using Domain.DTO;
using Domain.IRepository;
using Repository.Context;

namespace Repository.Repository
{
    public class PaymentFormRepository : BaseRepository<PaymentFormDTO>, IPaymentFormRepository
    {
        public PaymentFormRepository(QuickBuyContext quickBuyContext) : base(quickBuyContext)
        {

        }
    }
}