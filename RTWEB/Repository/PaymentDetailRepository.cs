using ZPWEB.Data;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public class PaymentDetailRepository : IPaymentDetailRepository
    {
        protected readonly Db _db;
        public PaymentDetailRepository(Db db)
        {
            _db = db;
        }

        public IEnumerable<PaymentDetailsVM> GetAll()
        {
            var data=(from p in _db.PaymentDetails).ToList();

            return data;
        }
    }
}
