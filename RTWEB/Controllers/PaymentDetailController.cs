using Microsoft.AspNetCore.Mvc;
using ZPWEB.Helpers;
using ZPWEB.Repository;

namespace ZPWEB.Controllers
{
    public class PaymentDetailController : Controller
    {
        protected readonly IUnitofWork _unitofWork;
        public PaymentDetailController(IUnitofWork unitofWork) 
        { 
            _unitofWork = unitofWork;
        }


        public IActionResult Index(int page=1 , int pageSize=10)
        {
            var data =_unitofWork.PaymentDetailRepository.GetAll()
                        .OrderByDescending(x=>x.Id)
                        .AsQueryable()
                        .ToPagedList(page, pageSize);
            return View(data);
        }
    }
}
