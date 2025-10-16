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
            var schedule=_unitofWork.ScheduleRepository.TransactionCheck(id);
            if (schedule)
            {
                TempData["Message"] = "❌ It is not possible to delete the schedule.";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            _unitofWork.ScheduleRepository.Delete(id);
            _unitofWork.Complete();
            TempData["Message"] = "✅ Schedule has been Delete";
            TempData["MessageType"] = "success";

            return RedirectToAction("Index");
        }

        public IActionResult Edit(int id)
        {
            var schedule=_unitofWork.ScheduleRepository.GetById(id);
            return View(schedule);
        }

        [HttpPost]
        public IActionResult Update(Schedule schedule)
        {
            if (schedule == null || string.IsNullOrEmpty(schedule.Name))
            {
                TempData["Message"] = "❌ Invalid data submitted";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }
            var isExisting = _unitofWork.ScheduleRepository
                .DuplicateCheck(schedule.Name); 

            if (isExisting)
            {
                TempData["Message"] = "❌ Schedule name already exists";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

            _unitofWork.ScheduleRepository.Update(schedule);
            var result = _unitofWork.Complete();

            if (result > 0)
            {
                TempData["Message"] = "✅ Schedule has been updated successfully";
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
