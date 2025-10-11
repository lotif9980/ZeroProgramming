using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
    [Authorize]
    public class InstractorController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public InstractorController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

         
        public IActionResult Index(int pageSize=10, int page=1)
        {
            var data =_unitofWork.InstractorRepository.GetAll()
                        .OrderByDescending(x=>x.Id)
                        .AsQueryable()
                        .ToPagedList(page, pageSize);
            return View(data);
        }

        [HttpGet]
        public IActionResult Save()
        {
            var model = new Instractor
            {
                Code=_unitofWork.InstractorRepository.GenerateCode(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Save(Instractor instractor)
        {
            bool exestingName =  _unitofWork.InstractorRepository.checkDuplicate(instractor.Name, instractor.ContactNo);
            if (exestingName)
            {
                TempData["Message"] = "❌ Already Instractor Added";
                TempData["MessageType"] = "danger";
                return View(instractor);
            }


            if (ModelState.IsValid)
            {
                _unitofWork.InstractorRepository.Save(instractor);
                _unitofWork.Complete();
                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";
                return RedirectToAction("Save");
            }
            else
            {
                return View(instractor);
            }
           
        }

        public IActionResult Delete(int id)
        {
            _unitofWork.InstractorRepository.Delete(id);
            var result=  _unitofWork.Complete();

            if (result >0)
            {
                TempData["Message"] = "✅ Delete Successful";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Delete fail";
                TempData["MessageType"] = "danger";
            }

            return RedirectToAction("Index");
        }
    }
}
