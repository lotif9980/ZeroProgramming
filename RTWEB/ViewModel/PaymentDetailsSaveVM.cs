using ZPWEB.Models;

namespace ZPWEB.ViewModel
{
    public class PaymentDetailsSaveVM
    {
        public PaymentDetail PaymentDetail { get; set; }
        public IEnumerable<IndexEnrollment> Enrollments { get; set; }
        public IEnumerable<Method> Methods { get; set; }
    }
}
