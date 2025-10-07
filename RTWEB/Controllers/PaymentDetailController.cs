using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;
using ZPWEB.ViewModel;

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

        [HttpGet]
        public IActionResult Save()
        {
            var data = new PaymentDetailsSaveVM
            {
              
                PaymentDetail = new Models.PaymentDetail
                {
                    Code=_unitofWork.PaymentDetailRepository.GenerateCode()
                },
                Enrollments=_unitofWork.EnrollmentRepository.GetAll(),
                Methods=_unitofWork.MethodRepository.GetAll(),
            };
            return View(data);
        }
    }
}
