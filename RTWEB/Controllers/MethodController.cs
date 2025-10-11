using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
    [Authorize]
    public class MethodController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public MethodController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }


        public IActionResult Index(int page=1,int pageSize=10)
        {
            var data= _unitofWork.MethodRepository.GetAll()
                    .OrderBy(d=>d.Id)
                    .AsQueryable()
                    .ToPagedList(page, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var vm = new Method
            {
                Code = _unitofWork.MethodRepository.GenerateCode(),
            };
            return View(vm);
        }

        [HttpPost]
        public IActionResult Save(Method method)
        {
            if (ModelState.IsValid)
            {
                bool isDuplicate = _unitofWork.MethodRepository.DuplicateCheck(method.Name);

                if (isDuplicate)
                {
                    TempData["Message"] = "❌ All ready Added";
                    TempData["MessageType"] = "danger";
                    return View(method);
                }

                _unitofWork.MethodRepository.Save(method);
                var result = _unitofWork.Complete();

                if (result > 0)
                {
                    TempData["Message"] = "✅ has been saved successfully.";
                    TempData["MessageType"] = "success";
                }
                else
                {
                    TempData["Message"] = "❌ Save Failed";
                    TempData["MessageType"] = "danger";
                }
                return RedirectToAction("Save");
            }
            return View(method);
        }

        public IActionResult Delete(int id)
        {
            bool exestingData=_unitofWork.MethodRepository.TransactionCheck(id);
            if (exestingData)
            {
                TempData["Message"] = "⚠️ Can't delete this Method — related Payment Details record(s) exist!";
                TempData["MessageType"] = "danger";
                return RedirectToAction("Index");
            }

             _unitofWork.MethodRepository.Delete(id);
            var result= _unitofWork.Complete();
            if(result > 0)
            {
                TempData["Message"] = "✅ Delete successful.";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Delete Failed";
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("Index");
        }


        public IActionResult Edit(int id)
        {
            var method = _unitofWork.MethodRepository.GetById(id);
            return View(method);
        }

    }
}
