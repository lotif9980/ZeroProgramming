using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;
using ZPWEB.ViewModel;

namespace ZPWEB.Controllers
{
    public class StudentController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        private IWebHostEnvironment _env;
        public StudentController(IUnitofWork unitofWork, IWebHostEnvironment env)
        {
            _unitofWork = unitofWork;
            _env = env;
        }
        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data =_unitofWork.StudentRepository.GetAll()
                       .OrderByDescending(x=>x.Id)
                       .AsQueryable()
                       .ToPagedList(page, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
           
            var vm = new StudentVM
            {
                Student= new Models.Student
                {
                    Code=_unitofWork.StudentRepository.GenerateCode(),
                },
                Courses=_unitofWork.CourseRepository.GetAll(),
                Schedules=_unitofWork.ScheduleRepository.GetAll(),
                Methods=_unitofWork.MethodRepository.GetAll()
            };
            return View(vm);
        }

        #region Old Save Method
        [HttpPost]
        public IActionResult SaveOld(StudentVM model)
        {

            if (string.IsNullOrWhiteSpace(model.Student.Name) ||
                string.IsNullOrWhiteSpace(model.Student.ContactNo) ||
                string.IsNullOrWhiteSpace(model.Student.Address))
            {
                TempData["Message"] = "❌ Invalid Data";
                TempData["MessageType"] = "danger";
                return ReturnStudentView(model);
            }


            bool isExisting = _unitofWork.StudentRepository
                .DuplicateCheck(model.Student.Name, model.Student.ContactNo);

            if (isExisting)
            {
                TempData["Message"] = "❌ Already Added";
                TempData["MessageType"] = "danger";
                return ReturnStudentView(model);
            }


            var student = new Student
            {
                Code = model.Student.Code,
                Name = model.Student.Name,
                ContactNo = model.Student.ContactNo,
                Address = model.Student.Address,
                FatherName = model.Student.FatherName,
                FatherPhoneNumber = model.Student.FatherPhoneNumber,
                MotherName = model.Student.MotherName,
                SchoolName = model.Student.SchoolName,
                Class = model.Student.Class
            };

            _unitofWork.StudentRepository.Save(student);
            int result = _unitofWork.Complete();

            if (result <= 0)
            {
                TempData["Message"] = "❌ Save Failed";
                TempData["MessageType"] = "danger";
                return ReturnStudentView(model);
            }


            if (model.Student.ImageFile != null)
            {
                string code = model.Student.Code.Replace(" ", "_");
                string extension = Path.GetExtension(model.Student.ImageFile.FileName);
                string fileName = $"{student.Id}_{code}{extension}";

                SavePhoto(model.Student.ImageFile, fileName);
                student.Picture = fileName;

                _unitofWork.StudentRepository.Update(student);
                _unitofWork.Complete();
            }


            if (student.Id != null && model.SelectCourceId != null && model.ScheduleId != null)
            {
                var enrollment = new Enrollment
                {
                    Code = _unitofWork.StudentRepository.GenerateCode(),
                    EnrollDate = DateTime.Now.Date,
                    StudentId = student.Id,
                    CourseId = model.SelectCourceId,
                    ScheduleId = model.ScheduleId,
                    TotalFee = model.TotalFee,
                    PaidAmount = model.PaidAmount,
                    DueAmount = model.DueAmount,
                };
                _unitofWork.EnrollmentRepository.Save(enrollment);
                _unitofWork.Complete();

                var paymentDetails = new PaymentDetail
                {
                    Code = _unitofWork.PaymentDetailRepository.GenerateCode(),
                    EnrollmentId = enrollment.Id,
                    PaymentDate = DateTime.Now.Date,
                    Amount = model.PaidAmount,
                    PaymentMethod = model.MethodId
                };

                _unitofWork.PaymentDetailRepository.Save(paymentDetails);
                _unitofWork.Complete();
            }


            TempData["Message"] = "✅ Save Successful";
            TempData["MessageType"] = "success";
            return RedirectToAction("Save");
        }

        #endregion Old Save End


        [HttpPost]
        public IActionResult Save(StudentVM model)
        {
            if (!IsStudentValid(model))
            {
                TempData["Message"] = "❌ Invalid Data";
                TempData["MessageType"] = "danger";
                return ReturnStudentView(model);
            }

            if (IsStudentDuplicate(model))
            {
                TempData["Message"] = "❌ Already Added";
                TempData["MessageType"] = "danger";
                return ReturnStudentView(model);
            }

            var student = CreateStudent(model);
            SaveStudentPhoto(student, model.Student.ImageFile);
            SaveEnrollmentAndPayment(student, model);

            TempData["Message"] = "✅ Save Successful";
            TempData["MessageType"] = "success";
            return RedirectToAction("Save");
        }

        private bool IsStudentValid(StudentVM model)
        {
            return !(string.IsNullOrWhiteSpace(model.Student.Name) ||
                     string.IsNullOrWhiteSpace(model.Student.ContactNo) ||
                     string.IsNullOrWhiteSpace(model.Student.Address));
        }

        private bool IsStudentDuplicate(StudentVM model)
        {
            return _unitofWork.StudentRepository.DuplicateCheck(model.Student.Name, model.Student.ContactNo);
        }

        private Student CreateStudent(StudentVM model)
        {
            var student = new Student
            {
                Code = model.Student.Code,
                Name = model.Student.Name,
                ContactNo = model.Student.ContactNo,
                Address = model.Student.Address,
                FatherName = model.Student.FatherName,
                FatherPhoneNumber = model.Student.FatherPhoneNumber,
                MotherName = model.Student.MotherName,
                SchoolName = model.Student.SchoolName,
                Class = model.Student.Class
            };

            _unitofWork.StudentRepository.Save(student);
            _unitofWork.Complete();
            return student;
        }

        private void SaveStudentPhoto(Student student, IFormFile file)
        {
            if (file == null) return;

            string code = student.Code.Replace(" ", "_");
            string extension = Path.GetExtension(file.FileName);
            string fileName = $"{student.Id}_{code}{extension}";

            SavePhoto(file, fileName);
            student.Picture = fileName;

            _unitofWork.StudentRepository.Update(student);
            _unitofWork.Complete();
        }

        private void SaveEnrollmentAndPayment(Student student, StudentVM model)
        {
            if (student.Id == null || model.SelectCourceId == null || model.ScheduleId == null) return;

            var enrollment = new Enrollment
            {
                Code = _unitofWork.StudentRepository.GenerateCode(),
                EnrollDate = DateTime.Now.Date,
                StudentId = student.Id,
                CourseId = model.SelectCourceId,
                ScheduleId = model.ScheduleId,
                TotalFee = model.TotalFee,
                PaidAmount = model.PaidAmount,
                DueAmount = model.DueAmount,
            };
            _unitofWork.EnrollmentRepository.Save(enrollment);
            _unitofWork.Complete();

            if (enrollment.PaidAmount == 0) return;

            var paymentDetails = new PaymentDetail
            {
                Code = _unitofWork.PaymentDetailRepository.GenerateCode(),
                EnrollmentId = enrollment.Id,
                PaymentDate = DateTime.Now.Date,
                Amount = model.PaidAmount,
                PaymentMethod = model.MethodId
            };
            _unitofWork.PaymentDetailRepository.Save(paymentDetails);
            _unitofWork.Complete();
        }


        public IActionResult Delete(int id)
        {
            _unitofWork.StudentRepository.Delete(id);
           var result=_unitofWork.Complete();

            if (result > 0)
            {
                TempData["Message"] = "✅ Delete Successful";
                TempData["MessageType"] = "success";

            }
            else
            {
                TempData["Message"] = "❌ Delete Faild";
                TempData["MessageType"] = "danger";
            }
            
            return RedirectToAction("Index");
        }

        private void SavePhoto(IFormFile file, string name)
        {
            string path = Path.Combine(_env.ContentRootPath, "wwwroot", "Students");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            string fullPath = Path.Combine(path, name);

            using (FileStream fs = System.IO.File.Create(fullPath))
            {
                file.CopyTo(fs);
                fs.Flush();
            }
        }

        private IActionResult ReturnStudentView(StudentVM model)
        {
            var vm = new StudentVM
            {
                Student = new Student
                {
                    Code = _unitofWork.StudentRepository.GenerateCode(),
                    Name = model.Student.Name,
                    ContactNo = model.Student.ContactNo,
                    Address = model.Student.Address,
                    FatherName = model.Student.FatherName,
                    FatherPhoneNumber = model.Student.FatherPhoneNumber,
                    MotherName = model.Student.MotherName,
                    SchoolName = model.Student.SchoolName,
                    Class = model.Student.Class
                },
                Courses = _unitofWork.CourseRepository.GetAll(),
                Schedules = _unitofWork.ScheduleRepository.GetAll(),
                Methods = _unitofWork.MethodRepository.GetAll()
            };

            return View("Save", vm);
        }
    }
}
