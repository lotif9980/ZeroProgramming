using ZPWEB.Models;
using ZPWEB.ViewModel;

namespace ZPWEB.Repository
{
    public interface IPaymentDetailRepository
    {
        public IEnumerable<PaymentDetailsVM> GetAll();
        public string GenerateCode();
        public Enrollment GetById(int id);
        public void Save(PaymentDetail model);
    }
}
