using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBook.Models.ViewModels;
using BulkyBook.Utility;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
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
                shoppingCarts = _unitOfWork.shoppingCartRepository.GetAll(x => x.ApplicationUserId == claims.Value, includeProperties: "product")
            };
            shoppingCartVM.orderHeader.OrderTotal = 0;
            shoppingCartVM.orderHeader.applicationUser = _unitOfWork.applicationUser.GetFirstorDefault(x => x.Id == claims.Value,
                includeProperties: "company");

            foreach (var cartList in shoppingCartVM.shoppingCarts)
            {
                cartList.Price = SD.GetPriceBasedOnQuantity(cartList.Count, cartList.product.Price, cartList.product.Price50, cartList.product.Price100);
                shoppingCartVM.orderHeader.OrderTotal += (cartList.Count * cartList.Price);
                cartList.product.Description = SD.ConvertToRawHtml(cartList.product.Description);
                if (cartList.product.Description.Length > 100)
                {
                    cartList.product.Description = cartList.product.Description.Substring(0, 99) + "...";
                }
            }

            return View(shoppingCartVM);
        }
        [HttpPost]
        [ActionName("Index")]
        public async Task<IActionResult> IndexPost()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claim = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);
            var user = _unitOfWork.applicationUser.GetFirstorDefault(x => x.Id == claim.Value);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "Verification Email is Empty");
            }

            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var callbackUrl = Url.Page(
                "/Account/ConfirmEmail",
                pageHandler: null,
                values: new { userId = user.Id, code = code },
                protocol: Request.Scheme);
            await _emailSender.SendEmailAsync(
                user.Email,
                "Confirm your email",
                $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

            ModelState.AddModelError(string.Empty, "Verification email sent. Please check your email.");
            return RedirectToAction("Index");
        }
        public IActionResult Plus(int cartId)
        {
            var cart = _unitOfWork.shoppingCartRepository.GetFirstorDefault(
                c => c.id == cartId, includeProperties: "product"
                );
            cart.Count += 1;
            cart.Price = SD.GetPriceBasedOnQuantity(cart.Count, cart.product.Price,
                cart.product.Price50, cart.product.Price100);
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
        }

        public IActionResult Minus(int cartId)
        {
            var cart = _unitOfWork.shoppingCartRepository.GetFirstorDefault(
                c => c.id == cartId, includeProperties: "product"
                );
            if (cart.Count == 1)
            {
                var cnt = _unitOfWork.shoppingCartRepository.GetAll(
                    u => u.ApplicationUserId == cart.ApplicationUserId
                    ).Count();
                _unitOfWork.shoppingCartRepository.Remove(cart);
                _unitOfWork.Save();
                HttpContext.Session.SetInt32(SD.ssShoppingCart, cnt - 1);
            }
            else
            {
                cart.Count -= 1;
                cart.Price = SD.GetPriceBasedOnQuantity(cart.Count, cart.product.Price,
                    cart.product.Price50, cart.product.Price100);
                _unitOfWork.Save();
            }

            return RedirectToAction(nameof(Index));
        }

        public IActionResult Remove(int cartId)
        {
            var cart = _unitOfWork.shoppingCartRepository.GetFirstorDefault(
                c => c.id == cartId, includeProperties: "product"
                );

            var cnt = _unitOfWork.shoppingCartRepository.GetAll(
                u => u.ApplicationUserId == cart.ApplicationUserId
                ).Count();
            _unitOfWork.shoppingCartRepository.Remove(cart);
            _unitOfWork.Save();
            HttpContext.Session.SetInt32(SD.ssShoppingCart, cnt - 1);


            return RedirectToAction(nameof(Index));
        }

        public IActionResult Summary()
        {
            var claimsIdentity = (ClaimsIdentity)User.Identity;
            var claims = claimsIdentity.FindFirst(ClaimTypes.NameIdentifier);

            shoppingCartVM = new ShoppingCartVM
            {
                orderHeader = new Models.OrderHeader(),
                shoppingCarts = _unitOfWork.shoppingCartRepository.GetAll
                (c => c.ApplicationUserId == claims.Value)
            };


            shoppingCartVM.orderHeader.applicationUser = _unitOfWork.applicationUser
                .GetFirstorDefault(c => c.Id == claims.Value, includeProperties: "company");

            foreach (var cartList in shoppingCartVM.shoppingCarts)
            {
                cartList.Price = SD.GetPriceBasedOnQuantity(cartList.Count, cartList.product.Price, cartList.product.Price50, cartList.product.Price100);
                shoppingCartVM.orderHeader.OrderTotal += (cartList.Count * cartList.Price);
            }

            shoppingCartVM.orderHeader.Name = shoppingCartVM.orderHeader.applicationUser.Name;
            shoppingCartVM.orderHeader.PhonNumber = shoppingCartVM.orderHeader.applicationUser.PhoneNumber;
            shoppingCartVM.orderHeader.StreetAddress = shoppingCartVM.orderHeader.applicationUser.StreetAddress;
            shoppingCartVM.orderHeader.City = shoppingCartVM.orderHeader.applicationUser.City;
            shoppingCartVM.orderHeader.State = shoppingCartVM.orderHeader.applicationUser.State;
            shoppingCartVM.orderHeader.PostalCode = shoppingCartVM.orderHeader.applicationUser.PostalCode;

            return View(shoppingCartVM);
        }
    }
}
