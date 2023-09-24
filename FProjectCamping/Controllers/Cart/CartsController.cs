using FProjectCamping.Models.Services;
using FProjectCamping.Models.ViewModels.Carts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.ApplicationServices;
using System.Web.Mvc;

namespace FProjectCamping.Controllers.Cart
{
	public class CartsController : Controller
	{
		private readonly Models.Services.ProfileService _profileService = new Models.Services.ProfileService();
		private readonly CartService _cartService = new CartService();

		// GET: Cart
		public ActionResult Cart()
		{
			// memberId 也許是從登入資訊來
			var model = _cartService.Get("aliee");

			return View(model);
		}

		[HttpPost]
		public ActionResult Cart(CartVm vm)
		{
			_cartService.Update(vm);

			return RedirectToAction("OrderInfo");
		}

		public ActionResult OrderInfo()
		{
			var cart = _cartService.Get("aliee");

			// 預設帶入 訂房人資料
			cart.ContactProfile = _profileService.GetMemberProfile("aliee");
			return View(cart);
		}

		[HttpPost]
		public ActionResult OrderInfo(CartVm vm)
		{
			if (!ModelState.IsValid) return View(vm);

			//var buyer = User.Identity.Name;
			var buyer = "aliee";
			var cart = _cartService.GetOrCreateCart(buyer);
			cart.AllowCheckout = cart.Items.Any();

			if (cart.AllowCheckout == false)
			{
				ModelState.AddModelError("", "購物車是空的,無法進行結帳");
				return View(vm);
			}

			cart.ContactProfile = vm.ContactProfile;
			cart.Coupon = vm.Coupon;
			cart.PaymentType = vm.PaymentType;

			_cartService.ProcessCheckout(buyer, cart);
			return RedirectToAction("Pay", "Orders"); // todo 轉導到結帳頁
		}

		/// <summary>
		/// 將 CartItem加入購物車
		/// </summary>
		/// <param name="vm"></param>
		/// <returns></returns>
		[Authorize]
		[HttpPost]
		public string AddItem(CartItemsVm vm)
		{
			string buyer = User.Identity.Name; // 買家帳號
			var result = _cartService.AddToCart(buyer, vm); //加入購物車

			return result; //沒傳回任何東西
		}

		[Authorize]
		public ActionResult Checkout()
		{
			var buyer = User.Identity.Name;
			var cart = _cartService.GetOrCreateCart(buyer);

			if (cart.AllowCheckout == false) ViewBag.ErrorMessage = "購物車是空的,無法進行結帳";

			return View();
		}

		[Authorize]
		[HttpPost]
		public ActionResult Checkout(CheckoutVm vm)
		{
			if (!ModelState.IsValid) return View(vm);

			var cartService = _cartService;

			var buyer = User.Identity.Name;
			var cart = cartService.GetOrCreateCart(buyer);

			if (cart.AllowCheckout == false)
			{
				ModelState.AddModelError("", "購物車是空的,無法進行結帳");
				return View(vm);
			}

			//cartService.ProcessCheckout(buyer, cart, vm);
			return View("ConfirmCheckout"); // todo 轉導到結帳頁
		}

		public ActionResult DeleteCartItem(int cartItemId)
		{
			_cartService.DeleteCartItem(cartItemId);
			return new EmptyResult();
		}
	}
}