namespace ZPWEB.Models
{
    public class Enrollment
    {
        public int Id { get; set; }
        public string? Code {  get; set; }
        public DateTime? EnrollDate {  get; set; }
        public int? StudentId { get; set; }
        public int? CourseId { get; set; }
        public int? ScheduleId {  get; set; }
        public decimal? TotalFee { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? DueAmount { get; set; }
        public int? Status { get; set; }
    }
}
