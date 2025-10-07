namespace ZPWEB.ViewModel
{
    public class PaymentDetailsVM
    {
        public int Id {  get; set; }
        public string? StudentName {  get; set; }
        public string? ContactNo { get; set; }
        public Decimal? PaidAmt { get; set; }
        public string? PaymentMethod {  get; set; }
        public DateTime? PaymentDate { get; set; }
        public int? EnrollmentId {  get; set; }
    }
}
