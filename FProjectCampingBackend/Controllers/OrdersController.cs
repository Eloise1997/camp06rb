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
        public ActionResult Index(Order vm)
        {
            var orders = db.Orders.Include(o => o.Member).Include(o => o.PaymentType);

            var resule = new List<OrderVm>();
            foreach (var item in orders)
            {
                var orderVm = new OrderVm()
                {
                    //OrderNumber = vm.OrderNumber // 考慮是否要增加
                    OrderTime = vm.OrderTime.ToShortDateString(),
                    PaymentType = vm.PaymentType.Name,
                    Price = vm.TotalPrice,
                    Status = vm.Status,
                    Name = vm.Name,
                    Email = vm.Email,
                    PhoneNum = vm.PhoneNum
                };
                resule.Add(orderVm);
            }
            return View(resule);
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
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.MemberId = new SelectList(db.Members, "Id", "Account");
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // 若要免於大量指派 (overposting) 攻擊，請啟用您要繫結的特定屬性，
        // 如需詳細資料，請參閱 https://go.microsoft.com/fwlink/?LinkId=317598。
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,MemberId,OrderTime,Status,PaymentTypeId,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.MemberId = new SelectList(db.Members, "Id", "Account", order.MemberId);
            ViewBag.PaymentTypeId = new SelectList(db.PaymentTypes, "Id", "Name", order.PaymentTypeId);
            return View(order);
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

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
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
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
            db.SaveChanges();
            return RedirectToAction("Index");
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