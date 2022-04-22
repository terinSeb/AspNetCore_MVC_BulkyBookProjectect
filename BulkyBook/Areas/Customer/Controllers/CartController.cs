using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace BulkyBook.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class CartController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IEmailSender _emailSender;
        private readonly UserManager<IdentityUser> _userManager;

        public ShoppingCartVM shoppingCartVM { get; set; }

        public CartController(IUnitOfWork unitOfWork, IEmailSender emailSender, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _emailSender = emailSender;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            shoppingCartVM = new ShoppingCartVM()
            {
                orderHeader = new OrderHeader(),
                shoppingCarts = _unitOfWork.shoppingCartRepository.GetAll(x => x.ApplicationUserId == claims.Value,includeProperties: "product")
            };
            shoppingCartVM.orderHeader.OrderTotal = 0;
            shoppingCartVM.orderHeader.applicationUser = _unitOfWork.applicationUser.GetFirstorDefault(x => x.Id == claims.Value,
                includeProperties:"company");

            foreach(var cartList in shoppingCartVM.shoppingCarts)
            {
                cartList.Price = SD.GetPriceBasedOnQuantity(cartList.Count, cartList.product.Price, cartList.product.Price50, cartList.product.Price100);
                shoppingCartVM.orderHeader.OrderTotal += (cartList.Count * cartList.Price);
                cartList.product.Description = SD.ConvertToRawHtml(cartList.product.Description);
                if(cartList.product.Description.Length > 100)
                {
                    cartList.product.Description = cartList.product.Description.Substring(0, 99) + "...";
                }
            }

            return View(shoppingCartVM);
        }
    }
}
