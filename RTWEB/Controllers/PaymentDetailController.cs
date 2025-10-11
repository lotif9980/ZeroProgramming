using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using ZPWEB.Enum;
using ZPWEB.Helpers;
using ZPWEB.Models;
using ZPWEB.Repository;
using ZPWEB.ViewModel;

namespace ZPWEB.Controllers
{
    [Authorize]
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
                Enrollments=_unitofWork.EnrollmentRepository.GetDueAmtount(),
                Methods=_unitofWork.MethodRepository.GetAll(),
            };
            return View(data);
        }

        #region Old Save Model
        [HttpPost]
        public IActionResult SaveOld(PaymentDetailsSaveVM model)
        {
            if (model.PaymentDetail == null ||
                    string.IsNullOrEmpty(model.PaymentDetail.Code) ||
                    model.PaymentDetail.EnrollmentId == null ||
                    model.PaymentDetail.PaymentMethod == null ||
                    model.PaymentDetail.Amount == 0)
            {
                TempData["Message"] = "❌ Invalid Data submit";
                model.Enrollments = _unitofWork.EnrollmentRepository.GetDueAmtount();
                model.Methods = _unitofWork.MethodRepository.GetAll();
                return View(model);
            } 

            var data = new PaymentDetail
            {
                Code=model.PaymentDetail.Code,
                EnrollmentId=model.PaymentDetail.EnrollmentId,
                PaymentDate=model.PaymentDetail.PaymentDate,
                Amount=model.PaymentDetail.Amount,
                PaymentMethod=model.PaymentDetail.PaymentMethod,
                TransactionId=model.PaymentDetail.TransactionId,
                Note= "Payment at the time of payment",
                Status= EnumForPaymentDetailsType.NormalPayment

            };

            _unitofWork.PaymentDetailRepository.Save(data);

            var enrollment= _unitofWork.EnrollmentRepository.GetById(model.PaymentDetail.EnrollmentId);
            if (enrollment != null)
            {
                enrollment.PaidAmount += model.PaymentDetail.Amount;
                enrollment.DueAmount =enrollment.TotalFee-enrollment.PaidAmount;

                _unitofWork.EnrollmentRepository.Update(enrollment);
            }

            var result=_unitofWork.Complete();
            if (result > 0)
            {
                TempData["Message"] = "✅ Save Successfull";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Save Faild";
                TempData["MessageType"] = "danger";
            }
            return RedirectToAction("Save");
        }
        #endregion

        [HttpPost]
        public IActionResult Save(PaymentDetailsSaveVM model)
        {
            if (!IsPaymentValid(model))
            {
                TempData["Message"] = "❌ Invalid Data submit";
                return ReturnPaymentView(model);
            }

            var payment = CreatePaymentDetail(model);
            _unitofWork.PaymentDetailRepository.Save(payment);

            UpdateEnrollmentAfterPayment(model.PaymentDetail.EnrollmentId, model.PaymentDetail.Amount);

            int result = _unitofWork.Complete();
            SetTempMessage(result > 0);

            return RedirectToAction("Save");
        }



        [HttpGet]
        public IActionResult GetEnrollmentLastDue(int enrollmentId)
        {
            var data=_unitofWork.PaymentDetailRepository.GetById(enrollmentId);
            if (data == null) return Json(0);
            return Json(data.DueAmount);
        }


        private bool IsPaymentValid(PaymentDetailsSaveVM model)
        {
            var pd = model.PaymentDetail;
            return !(pd == null ||
                     string.IsNullOrEmpty(pd.Code) ||
                     pd.EnrollmentId == null ||
                     pd.PaymentMethod == null ||
                     pd.Amount <= 0);
        }

        private IActionResult ReturnPaymentView(PaymentDetailsSaveVM model)
        {
            model.Enrollments = _unitofWork.EnrollmentRepository.GetDueAmtount();
            model.Methods = _unitofWork.MethodRepository.GetAll();
            return View(model);
        }

        private PaymentDetail CreatePaymentDetail(PaymentDetailsSaveVM model)
        {
            return new PaymentDetail
            {
                Code = model.PaymentDetail.Code,
                EnrollmentId = model.PaymentDetail.EnrollmentId,
                PaymentDate = model.PaymentDetail.PaymentDate,
                Amount = model.PaymentDetail.Amount,
                PaymentMethod = model.PaymentDetail.PaymentMethod,
                TransactionId = model.PaymentDetail.TransactionId,
                Note = "Payment at the time of payment",
                Status = EnumForPaymentDetailsType.NormalPayment
            };
        }

        private void UpdateEnrollmentAfterPayment(int enrollmentId, decimal amount)
        {
            var enrollment = _unitofWork.EnrollmentRepository.GetById(enrollmentId);
            if (enrollment == null) return;

            enrollment.PaidAmount += amount;
            enrollment.DueAmount = enrollment.TotalFee - enrollment.PaidAmount;

            _unitofWork.EnrollmentRepository.Update(enrollment);
        }


        private void SetTempMessage(bool success)
        {
            if (success)
            {
                TempData["Message"] = "✅ Save Successful";
                TempData["MessageType"] = "success";
            }
            else
            {
                TempData["Message"] = "❌ Save Failed";
                TempData["MessageType"] = "danger";
            }
        }

    }
}
