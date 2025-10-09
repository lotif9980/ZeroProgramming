using Microsoft.AspNetCore.Mvc;
using ZPWEB.Enum;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;
using ZPWEB.ViewModel;

namespace ZPWEB.Controllers
{
    public class EnrollmentController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public EnrollmentController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }


        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data =_unitofWork.EnrollmentRepository.GetAll()
                      .OrderByDescending(x=>x.Id)
                      .AsQueryable()
                      .ToPagedList(page,pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var data = new EnrollmentVM
            {
                Enrollment = new Models.Enrollment
                {
                    Code = _unitofWork.EnrollmentRepository.CreateGenerateCode()
                },
                Course = _unitofWork.CourseRepository.ActiveGetAll(),
                Students = _unitofWork.StudentRepository.GetAll(),
                Schedule = _unitofWork.ScheduleRepository.GetAll(),
                Method=_unitofWork.MethodRepository.GetAll()
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult Save(EnrollmentVM model)
        {
          
            if (model.Enrollment == null ||
                string.IsNullOrEmpty(model.Enrollment.Code) ||
                model.Enrollment.StudentId == null ||
                model.Enrollment.CourseId == null ||
                model.Enrollment.ScheduleId == null ||
                model.SelectedMethodId==null)
            {
                TempData["Message"] = "❌ Invalid Data Submit";
                TempData["MessageType"] = "danger";
                return ReturnEnrollmentView(model);
            }

          
            bool isDuplicate = _unitofWork.EnrollmentRepository
                .DuplicateCheck(model.Enrollment.StudentId.Value, model.Enrollment.CourseId.Value);

            if (isDuplicate)
            {
                TempData["Message"] = "❌ This student already enrolled in this course";
                TempData["MessageType"] = "danger";
                return ReturnEnrollmentView(model);
            }

           
            var data = new Enrollment
            {
                Code = model.Enrollment.Code,
                EnrollDate = model.Enrollment.EnrollDate,
                StudentId = model.Enrollment.StudentId,
                CourseId = model.Enrollment.CourseId,
                ScheduleId = model.Enrollment.ScheduleId,
                TotalFee = model.Enrollment.TotalFee,
                PaidAmount = model.Enrollment.PaidAmount,
                DueAmount = model.Enrollment.DueAmount,
                Status = model.Enrollment.Status
            };

            _unitofWork.EnrollmentRepository.Save(data);
            _unitofWork.Complete();

          
            var payment = new PaymentDetail
            {
                Code = _unitofWork.PaymentDetailRepository.GenerateCode(),
                EnrollmentId = data.Id,
                PaymentDate = model.Enrollment.EnrollDate,
                Amount = model.Enrollment.PaidAmount,
                PaymentMethod = model.SelectedMethodId,
                Note= "Payment at the time of enrollment",
                Status = EnumForPaymentDetailsType.Enrollment
            };

            _unitofWork.PaymentDetailRepository.Save(payment);

            int result = _unitofWork.Complete();

          
            if (result > 0)
            {
                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";
                return RedirectToAction("Save");
            }
            else
            {
                TempData["Message"] = "❌ Save Failed";
                TempData["MessageType"] = "danger";
                return ReturnEnrollmentView(model);
            }
        }

        private IActionResult ReturnEnrollmentView(EnrollmentVM model)
        {
            var vm = new EnrollmentVM
            {
                Enrollment = new Enrollment
                {
                    Code = _unitofWork.EnrollmentRepository.CreateGenerateCode(),
                    EnrollDate = DateTime.Now.Date,
                    TotalFee = model.Enrollment?.TotalFee,
                    PaidAmount = model.Enrollment.PaidAmount,
                    DueAmount = model.Enrollment?.DueAmount,
                    Status = model.Enrollment?.Status
                },
                Course = _unitofWork.CourseRepository.GetAll(),
                Students = _unitofWork.StudentRepository.GetAll(),
                Schedule = _unitofWork.ScheduleRepository.GetAll(),
                Method = _unitofWork.MethodRepository.GetAll()
            };

            return View("Save", vm);
        }

        public IActionResult Delete(int id)
        {
            _unitofWork.EnrollmentRepository.Delete(id);
            var result = _unitofWork.Complete();
            if (result > 0)
            {
                TempData["Message"] = "✅ Delete Successful";
                TempData["MessageType"]="success";
            }
            else
            {
                TempData["Message"] = "❌ Delete Faild";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult GetCourseFee(int courseId)
        {
            var course = _unitofWork.CourseRepository.GetById(courseId);
            if (course == null) return Json(0);
            return Json(course.CourseFee);
        }
    }
}
