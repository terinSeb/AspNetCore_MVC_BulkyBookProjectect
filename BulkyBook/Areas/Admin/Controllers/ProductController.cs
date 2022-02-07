using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Dapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using BulkyBook.DataAccess.Data;
using Microsoft.EntityFrameworkCore;
namespace BulkyBook.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        private readonly ApplicationDbContext _db;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment, ApplicationDbContext db)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
            _db = db;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Upsert(int? id)
        {
            ProductVM productVM = new ProductVM()
            {
                product = new Product(),
                CategoryList = _unitOfWork.Category.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }
                ),
                CoverTypeList = _unitOfWork.coverType.GetAll().Select(i => new SelectListItem
                {
                    Text = i.Name,
                    Value = i.Id.ToString()
                }
                )
            };
            if (id == null)
            {
                //this is for create
                return View(productVM);
            }
            //this is for edit
            productVM.product  = _unitOfWork.product.get(id.GetValueOrDefault());
            if (productVM.product == null)
            {
                return NotFound();
            }
            return View(productVM.product);
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
            var allObj = _unitOfWork.product.GetAll(includeProperties: "category,coverType");
           
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
