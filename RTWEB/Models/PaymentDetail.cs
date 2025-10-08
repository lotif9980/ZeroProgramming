namespace ZPWEB.Models
{
    public class PaymentDetail
    {
        public int Id { get; set; }
        public string? Code { get; set; }
        public int EnrollmentId {  get; set; }
        public decimal Amount { get; set; }
        public DateTime PaymentDate { get; set; }
        public int? PaymentMethod {  get; set; }
        public string? TransactionId {  get; set; }
        public string? Note { get; set; }
    }
}
