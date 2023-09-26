using FProjectCampingBackend.Models.EFModels;
using FProjectCampingBackend.Models.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;

namespace FProjectCampingBackend.Controllers
{
	public class OrdersController : Controller
	{
		private AppDbContext db = new AppDbContext();

		// GET: Orders
		public ActionResult Index()
		{
			var orders = db.Orders.Include(o => o.Member).Include(o => o.PaymentType);
			var result = new List<OrderVm>();
			foreach (var order in orders)
			{
				var orderVm = new OrderVm()
				{
					Id = order.Id,
					OrderTime = order.OrderTime,
					PaymentType = order.PaymentType.Name,
					Price = order.TotalPrice,
					Status = order.Status.ToString(), // todo
					Name = order.Name,
					Email = order.Email,
					PhoneNum = order.PhoneNum
				};
				result.Add(orderVm);
			}
			return View(result);
		}

		// GET: Orders/Details/5
		public ActionResult Details(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			var result = new List<OrderItemsVm>();
			foreach (var item in order.OrderItems)
			{
				var orderItemsVm = new OrderItemsVm
				{
					RoomType = item.Room.RoomType.Name,
					RoomNum = item.Room.RoomName,
					CheckInDate = item.CheckInDate.ToShortDateString(),
					CheckOutDate = item.CheckOutDate.ToShortDateString(),
					Days = item.Days,
					SubTotal = item.SubTotal
				};
				result.Add(orderItemsVm);
			}
			return View(result);
		}

		// GET: Orders/Edit/5
		public ActionResult Edit(int? id)
		{
			if (id == null)
			{
				return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
			}
			Order order = db.Orders.Find(id);
			if (order == null)
			{
				return HttpNotFound();
			}
			ViewBag.MemberId = new SelectList(db.Members, "Id", "Account", order.MemberId);
			ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", order.PaymentTypeId);
			return View(order);
		}

		// POST: Orders/Edit/5
		// 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
		// 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit([Bind(Include = "Id,MemberId,OrderTime,Status,PaymentTypeId,TotalPrice")] Order order)
		{
			if (ModelState.IsValid)
			{
				db.Entry(order).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("Index");
			}
			ViewBag.MemberId = new SelectList(db.Members, "Id", "Account", order.MemberId);
			ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", order.PaymentTypeId);
			return View(order);
		}

		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				db.Dispose();
			}
			base.Dispose(disposing);
		}
	}
}