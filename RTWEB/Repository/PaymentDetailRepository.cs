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
            var data=(from p in _db.PaymentDetails
                      join e in _db.Enrollments on p.EnrollmentId equals e.Id
                      join s in _db.Students on e.StudentId equals s.Id 
                      join pm in _db.Methods on p.PaymentMethod equals pm.Id
                      select new PaymentDetailsVM 
                      { 
                       Id=p.Id,
                       Code=p.Code,
                       PaymentDate=p.PaymentDate,
                       StudentName=s.Name,
                       ContactNo=s.ContactNo,
                       PaidAmt=p.Amount,
                       PaymentMethod=pm.Name
                      }).ToList();

            return data;
        }
    }
}
