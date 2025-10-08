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

        [HttpPost]
        public IActionResult Save(StudentVM model)
        {
            
                bool exestingCheck = _unitofWork.StudentRepository.DuplicateCheck(model.Student.Name, model.Student.ContactNo);
                if (exestingCheck)
                {
                    TempData["Message"] = "❌ Already Added";
                    TempData["MessageType"] = "danger";
                    return View(model);
                }

                var student = new Student
                {
                    Code=model.Student.Code,
                    Name=model.Student.Name,
                    ContactNo=model.Student.ContactNo,
                    Address=model.Student.Address,
                    FatherName=model.Student.FatherName,
                    FatherPhoneNumber=model.Student.FatherPhoneNumber,
                    MotherName=model.Student.MotherName,
                    SchoolName=model.Student.SchoolName,
                    Picture=model.Student.Picture,
                    Class=model.Student.Class
                };

                _unitofWork.StudentRepository.Save(student);
                var result = _unitofWork.Complete(); 

                if (result > 0)
                {
                 
                    if (model.Student.ImageFile != null)
                    {
                        string code = model.Student.Code.Replace(" ", "_");
                        string fileName = $"{model.Student.Id}_{code}{Path.GetExtension(model.Student.ImageFile.FileName)}";

                        SavePhoto(model.Student.ImageFile, fileName);
                        student.Picture = fileName;

                        _unitofWork.StudentRepository.Update(student);
                        _unitofWork.Complete();
                    }

                    TempData["Message"] = "✅ Save Successful";
                    TempData["MessageType"] = "success";
                    return RedirectToAction("Save");
                }
                else
                {
                    TempData["Message"] = "❌ Save Faild";
                    TempData["MessageType"] = "danger";
                    return View(student);
                }
           
            return View(model);
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
    }
}
