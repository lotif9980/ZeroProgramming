using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
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

        public IActionResult Save()
        {
            return View();
        }
    }
}
