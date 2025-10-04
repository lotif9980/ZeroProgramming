using Microsoft.AspNetCore.Mvc;
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
                //Enrollment = new Models.Enrollment(),
                Enrollment = new Models.Enrollment
                {
                    Code = _unitofWork.EnrollmentRepository.CreateGenerateCode()
                },
                Course = _unitofWork.CourseRepository.GetAll(),
                Students = _unitofWork.StudentRepository.GetAll(),
                Schedule = _unitofWork.ScheduleRepository.GetAll()
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
                    model.Enrollment.ScheduleId == null)
                {
                    TempData["Message"] = "❌ Invalid Data submit";
                    model.Course = _unitofWork.CourseRepository.GetAll();
                    model.Students = _unitofWork.StudentRepository.GetAll();
                    model.Schedule = _unitofWork.ScheduleRepository.GetAll();
                    return View(model);
                }

                bool isDuplicate = _unitofWork.EnrollmentRepository.DuplicateCheck(model.Enrollment.StudentId.Value,model.Enrollment.CourseId.Value);
                if (isDuplicate)
                {
                    TempData["Message"] = "❌ This student already enrolled in this course";
                    TempData["MessageType"] = "danger";

                    model.Course = _unitofWork.CourseRepository.GetAll();
                    model.Students = _unitofWork.StudentRepository.GetAll();
                    model.Schedule = _unitofWork.ScheduleRepository.GetAll();
                    return View(model);
                }

                var data = new Enrollment
                {
                    Code = model.Enrollment.Code,
                    EnrollDate = model.Enrollment.EnrollDate,
                    StudentId = model.Enrollment.StudentId,
                    CourseId = model.Enrollment.CourseId,
                    ScheduleId = model.Enrollment.ScheduleId
                };

           
                _unitofWork.EnrollmentRepository.Save(data);
               var result=_unitofWork.Complete();
                if (result > 0)
                {
                    TempData["Message"] = "✅ Save Successfull";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "❌ Save Faild";
                    TempData["MessageType"] = "danger";
                }
                return RedirectToAction("Save");
            }
    }
}
