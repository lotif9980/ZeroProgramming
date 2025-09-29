using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
    public class StudentController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public StudentController(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }
        public IActionResult Index(int page=1, int pageSize=10)
        {
            var data =_unitofWork.StudentRepository.GetAll()
                       .OrderByDescending(x=>x.Id)
                       .AsQueryable()
                       .ToPagedList(page, pageSize);
            return View(data);
        }
    }
}
