using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Company company = new Company();
            if (id == null)
            {
                //this is for create
                return View(company);
            }
            //this is for edit
            company = _unitOfWork.company.get(id.GetValueOrDefault());
            if (company == null)
            {
                return NotFound();
            }
            return View(company);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Company company)
        {
            if (ModelState.IsValid)
            {
                if (company.Id == 0)
                {
                    _unitOfWork.company.Add(company);
                }
                else
                {
                    _unitOfWork.company.Update(company);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(company);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.company.GetAll();
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var ObjFromDb = _unitOfWork.company.get(id);
            if (ObjFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitOfWork.company.Remove(ObjFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfull" });
        }
        #endregion
    }
}
