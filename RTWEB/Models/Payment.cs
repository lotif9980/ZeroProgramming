namespace ZPWEB.Models
{
    public class Payment
    {
        public int Id { get; set; }
        public int? EnrollmentId {  get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? PaymentMethodId {  get; set; }
    }
}
