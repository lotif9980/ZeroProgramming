using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
    public class EnrollmentController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public EnrollmentController(IUnitofWork unitofwork)
        {
            _unitofWork = unitofwork;
        }


        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data =_unitofWork.EnrollmentRepository.GetAll()
                      .OrderByDescending(x=>x.Id)
                      .AsQueryable()
                      .ToPagedList(page,pageSize);
            return View(data);
        }
    }
}
