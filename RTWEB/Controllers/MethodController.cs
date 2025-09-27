using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
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

    }
}
