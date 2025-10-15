using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;
using ZPWEB.ViewModel;

namespace ZPWEB.Controllers
{
    [Authorize]
    public class ScheduleController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public ScheduleController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }

        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data=_unitofWork.ScheduleRepository.GetAll()
                      .OrderBy(d=>d.Id)
                      .AsQueryable()
                      .ToPagedList(page, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var data = new Schedule
            {
                Code = _unitofWork.ScheduleRepository.GenerateCode(),
            };
            return View(data);
        }

        [HttpPost]
        public IActionResult Save(Schedule schedule)
        {
            if (ModelState.IsValid)
            {
                var isExesting = _unitofWork.ScheduleRepository.DuplicateCheck(schedule.Name);
                if (isExesting)
                {
                    TempData["Message"] = "❌ Schedule Already Added";
                    TempData["MessageType"] = "danger";
                    return View(schedule);
                }
                _unitofWork.ScheduleRepository.Save(schedule);
                _unitofWork.Complete();

                TempData["Message"] = "✅ Schedule has been saved successfully.";
                TempData["MessageType"] = "success";

                return RedirectToAction("Save");
            }
           return View(schedule);
        }

        public IActionResult Delete(int id)
        {

            _unitofWork.ScheduleRepository.Delete(id);
            _unitofWork.Complete();
            TempData["Message"] = "❌ Schedule has been Delete";
            TempData["MessageType"] = "danger";

            return RedirectToAction("Index");
        }
    }
}
