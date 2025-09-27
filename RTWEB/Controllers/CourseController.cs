using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
    public class CourseController : Controller
    {
        protected readonly IUnitofWork _unitofWork;

        public CourseController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }


        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data =_unitofWork.CourseRepository.GetAll()
                        .OrderBy(d=>d.Id)
                        .AsQueryable()
                        .ToPagedList(page, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var vm = new Course
            {
                Code=_unitofWork.CourseRepository.GenerateCode(),
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Save(Course course)
        {
            if (ModelState.IsValid)
            {
                bool exestingData = _unitofWork.CourseRepository.ExestingCheck(course.CourseName);
                if (exestingData)
                {
                    TempData["Message"] = "❌ already Added";
                    TempData["MessageType"] = "danger";
                    return View(course);
                }

                _unitofWork.CourseRepository.Save(course);
               var result= _unitofWork.Complete();
                if (result > 0)
                {
                    TempData["Message"] = "✅ Save Success";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "❌ Save failed";
                    TempData["MessageType"] = "danger";
                }

                return RedirectToAction("Save");
            }

            return View(course);
        }

        public IActionResult Delete(int id)
        {
          
            _unitofWork.CourseRepository.Delete(id);
            var result=_unitofWork.Complete();
            if(result > 0)
            {
                TempData["Message"] = "✅ Delete Successfuly";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Delete failed";
                TempData["MessageType"] = "danger";
            }
        
            return RedirectToAction("Index");
        }
    }
}
