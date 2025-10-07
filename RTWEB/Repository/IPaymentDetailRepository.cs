using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public interface IPaymentDetailRepository
    {
        public IEnumerable<PaymentDetailsVM> GetAll();
    }
}
