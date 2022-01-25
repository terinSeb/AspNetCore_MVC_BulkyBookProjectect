using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            Product product = new Product();
            if (id == null)
            {
                //this is for create
                return View(product);
            }
            //this is for edit
            var parameter = new DynamicParameters();
            parameter.Add("@id", id);
            product = _unitOfWork.product.get(id.GetValueOrDefault());
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Upsert(Product product)
        {
            //if (ModelState.IsValid)
            //{
            //    var parameter = new DynamicParameters();
            //    parameter.Add("@Name", product.Name);

            //    if (product.Id == 0)
            //    {
            //        _unitOfWork.SP_Call.Execute(SD.Proc_Product_Create, parameter);
            //    }
            //    else
            //    {
            //        parameter.Add("@id", product.Id);
            //        _unitOfWork.SP_Call.Execute(SD.Proc_Product_Update, parameter);
            //    }
            //    _unitOfWork.Save();
            //    return RedirectToAction(nameof(Index));
            //}
            return View(product);
        }

        #region API Calls
        [HttpGet]
        public IActionResult GetAll()
        {
            var allObj = _unitOfWork.product.GetAll();

            //--------------------------------Using Calling SP ---------------------------------

            //var allObj = _unitOfWork.SP_Call.List<Product>(SD.Proc_Products_GetAll, null);
            return Json(new { data = allObj });
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var ObjFromDb = _unitOfWork.product.get(id);
            if (ObjFromDb == null)
            {
                return Json(new { success = false, message = "Error While Deleting" });
            }
            _unitOfWork.product.Remove(ObjFromDb);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfull" });

            //---------------------------------Using Stored Procedure---------------------------------

            //var parameter = new DynamicParameters();
            //parameter.Add("@id", id);
            //var ObjFromDb = _unitOfWork.SP_Call.OneRecord<Product>(SD.Proc_Product_Get, parameter);
            //if (ObjFromDb == null)
            //{
            //    return Json(new { success = false, message = "Error While Deleting" });
            //}
            //_unitOfWork.SP_Call.Execute(SD.Proc_Product_Delete, parameter);
            //_unitOfWork.Save();
            //return Json(new { success = true, message = "Delete Successfull" });
        }
        #endregion
    }
}
