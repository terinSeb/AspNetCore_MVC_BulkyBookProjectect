using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CoverTypeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            CoverType cover = new CoverType();
            if (id == null)
            {
                //this is for create
                return View(cover);
            }
            //this is for edit
            var parameter = new DynamicParameters();
            parameter.Add("@id", id);
            cover = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
            if (cover == null)
            {
                return NotFound();
            }
            return View(cover);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(CoverType coverType)
        {
            if (ModelState.IsValid)
            {
                var parameter = new DynamicParameters();
                parameter.Add("@Name", coverType.Name);

                if (coverType.Id == 0)
                {
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Create, parameter);
                }
                else
                {
                    parameter.Add("@id", coverType.Id);
                    _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Update, parameter);
                }
                _unitOfWork.Save();
                return RedirectToAction(nameof(Index));
            }
            return View(coverType);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            //var allObj = _unitOfWork.coverType.GetAll();

            //--------------------------------Using Calling SP ---------------------------------

            var allObj = _unitOfWork.SP_Call.List<CoverType>(SD.Proc_CoverTypes_GetAll, null);
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            //var ObjFromDb = _unitOfWork.coverType.get(id);
            //if (ObjFromDb == null)
            //{
            //    return Json(new { success = false, message = "Error While Deleting" });
            //}
            //_unitOfWork.coverType.Remove(ObjFromDb);
            //_unitOfWork.Save();
            //return Json(new { success = true, message = "Delete Successfull" });

            //---------------------------------Using Stored Procedure---------------------------------

            var parameter = new DynamicParameters();
            parameter.Add("@id", id);
            var ObjFromDb = _unitOfWork.SP_Call.OneRecord<CoverType>(SD.Proc_CoverType_Get, parameter);
            if (ObjFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitOfWork.SP_Call.Execute(SD.Proc_CoverType_Delete, parameter);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfull" });
        }
        #endregion
    }
}
