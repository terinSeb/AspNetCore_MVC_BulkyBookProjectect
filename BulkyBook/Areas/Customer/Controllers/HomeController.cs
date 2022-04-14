
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(ILogger<HomeController> logger,IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Product> productList = _unitOfWork.product.GetAll(includeProperties: "category,coverType");

            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            if(claims != null)
            {
                int count = _unitOfWork.shoppingCartRepository.GetAll(u => u.ApplicationUserId == claims.Value).ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);
            }
            return View(productList);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult Details(int Id)
        {
            var ObjFromDb = _unitOfWork.product.GetFirstorDefault(x => x.Id == Id, includeProperties: "category,coverType");
            ShoppingCart ObjCart = new ShoppingCart()
            {
                product = ObjFromDb,
                ProductId = ObjFromDb.Id
            };
            return View(ObjCart);
        }
        [HttpPost]
        [AutoValidateAntiforgeryToken]
        [Authorize]
        public IActionResult Details(ShoppingCart ObjshoppingCart)
        {
            ObjshoppingCart.id = 0;
            if (ModelState.IsValid)
            {
                //then we will add to cart
                var claimsIdentity = (ClaimsIdentity)User.Identity;
                var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
                ObjshoppingCart.ApplicationUserId = claims.Value;
                ShoppingCart ObjCartfromDb = _unitOfWork.shoppingCartRepository.GetFirstorDefault(
                     x => x.ApplicationUserId == ObjshoppingCart.ApplicationUserId
                     && x.ProductId == ObjshoppingCart.ProductId,includeProperties: "product");
                if(ObjCartfromDb == null)
                {
                    // No product for that user exists
                    _unitOfWork.shoppingCartRepository.Add(ObjshoppingCart);
                }
                else
                {
                    ObjCartfromDb.Count += ObjshoppingCart.Count;
                    _unitOfWork.shoppingCartRepository.update(ObjCartfromDb);
                }                       
                _unitOfWork.Save();

                int count = _unitOfWork.shoppingCartRepository.GetAll(u => u.ApplicationUserId == ObjCartfromDb.ApplicationUserId).ToList().Count();
                HttpContext.Session.SetInt32(SD.ssShoppingCart, count);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                var ObjFromDb = _unitOfWork.product.GetFirstorDefault(x => x.Id == ObjshoppingCart.ProductId, includeProperties: "category,coverType");
                ShoppingCart ObjCart = new ShoppingCart()
                {
                    product = ObjFromDb,
                    ProductId = ObjFromDb.Id
                };
                return View(ObjCart);
            }
        }
    }
}
