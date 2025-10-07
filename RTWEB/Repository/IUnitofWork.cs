namespace ZPWEB.Repository
{
    public interface IUnitofWork : IDisposable
    {
        
        IInstractorRepository InstractorRepository { get; }
        ICourseRepository CourseRepository { get; }
        IMethodRepository MethodRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        IStudentRepository StudentRepository { get; }
        IEnrollmentRepository EnrollmentRepository { get; }
        IPaymentDetailRepository PaymentDetailRepository { get; }

        int Complete();
    }
}
