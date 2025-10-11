using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
    [Authorize]
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

                var data = new Course
                {
                    Code=course.Code,
                    CourseName=course.CourseName,
                    CourseFee=course.CourseFee,
                    Status=true,
                    Duration=course.Duration
                };

                _unitofWork.CourseRepository.Save(data);
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

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var student=_unitofWork.CourseRepository.GetById(id);
            if (student == null)
            {
                TempData["Message"] = "❌ data not found";
                TempData["MessageType"] = "danger";
            }
            return View(student);
        }

        public IActionResult Delete(int id)
        {
           bool exestingData = _unitofWork.CourseRepository.TransactionCheck(id);
            if (exestingData)
            {
                    TempData["Message"] = "⚠️ Can't delete this Course — related Enrollment record(s) exist!";
                    TempData["MessageType"] = "danger";

                return RedirectToAction("Index");
            }
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

        public IActionResult ToggleStatus(int id)
        {
            var data =_unitofWork.CourseRepository.GetById(id);
            if (data == null)
            {
                TempData["Message"] = "⚠️ Data not found!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            _unitofWork.CourseRepository.UpdateStatus(id);
            var result = _unitofWork.Complete();

            if (result > 0)
            {
                TempData["Message"] = "✅ Update Successfuly";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Update failed";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }


        public IActionResult Update(Course model)
        {
            _unitofWork.CourseRepository.Update(model);
            var result =_unitofWork.Complete();
            if (result > 0)
            {
                TempData["Message"] = "✅ Update Successfuly";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Update failed";
                TempData["MessageType"] = "danger";
            }
        

            return RedirectToAction("Index");
        }
    }
}
