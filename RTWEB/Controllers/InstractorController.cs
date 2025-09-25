using Microsoft.AspNetCore.Mvc;
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

         
        public IActionResult Index(int size=10, int page)
        {
            var data =_unitofWork.InstractorRepository.GetAll();
            return View(data);
        }
    }
}
