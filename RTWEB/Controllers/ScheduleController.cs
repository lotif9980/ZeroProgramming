using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
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
                      .OrderByDescending(d=>d.Id)
                      .AsQueryable()
                      .ToPagedList(page, pageSize);
            return View(data);
        }
    }
}
