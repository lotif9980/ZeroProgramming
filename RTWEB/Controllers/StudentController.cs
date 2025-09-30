using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;

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
            var vm = new Student
            {
                Code=_unitofWork.StudentRepository.GenerateCode()
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Save(Student student)
        {
            if (ModelState.IsValid)
            {
                bool exestingCheck = _unitofWork.StudentRepository.DuplicateCheck(student.Name, student.ContactNo);
                if (exestingCheck)
                {
                    TempData["Message"] = "❌ Already Added";
                    TempData["MessageType"] = "danger";
                    return View(student);
                }

                _unitofWork.StudentRepository.Save(student);
                var result = _unitofWork.Complete(); 

                if (result > 0)
                {
                 
                    if (student.ImageFile != null)
                    {
                        string code = student.Code.Replace(" ", "_");
                        string fileName = $"{student.Id}_{code}{Path.GetExtension(student.ImageFile.FileName)}";

                        SavePhoto(student.ImageFile, fileName);
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
            }
            return View(student);
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
