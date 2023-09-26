using FProjectCamping.Models.Services;
using FProjectCamping.Models.ViewModels.Orders;
using PayPal.Api;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FProjectCamping.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderService _orderService = new OrderService();

        // GET: Orders
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Paypal(int orderId, int totalPrice, string orderNumber)
        {
            // 設定APIContext
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);

            // 設定付款信息
            var payment = Payment.Create(apiContext, new Payment
            {
                intent = "sale",
                payer = new Payer { payment_method = "paypal" },
                transactions = new List<Transaction>
                {
                    new Transaction
                    {
                        description = "商品描述",
                        invoice_number = orderNumber,
                        amount = new Amount { currency = "TWD", total = totalPrice.ToString() }, // 設定金額和貨幣
                    }
                },
                redirect_urls = new RedirectUrls
                {
                    return_url = "http://localhost:yourport/Orders/CallBack", // 付款完成後返回的URL
                    cancel_url = $"http://localhost:yourport/Orders/Pay?orderId={orderId}" // 付款取消後返回的URL
                }
            });

            // 取得並轉導PayPal付款頁面
            var links = payment.links.GetEnumerator();
            string paypalRedirectUrl = null;
            while (links.MoveNext())
            {
                Links link = links.Current;
                if (link.rel.ToLower().Trim().Equals("approval_url"))
                {
                    paypalRedirectUrl = link.href;
                }
            }
            return Redirect(paypalRedirectUrl);
        }

        /// <summary>
        /// Paypal CallBack 接口
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public ActionResult CallBack()
        {
            // 從查詢參數中獲取付款ID和Payer ID
            var paymentId = Request.Params["paymentId"];
            var payerId = Request.Params["PayerID"];

            // 使用提供的ID來執行付款
            var config = ConfigManager.Instance.GetProperties();
            var accessToken = new OAuthTokenCredential(config).GetAccessToken();
            var apiContext = new APIContext(accessToken);
            var executedPayment = new Payment() { id = paymentId }.Execute(apiContext, new PaymentExecution { payer_id = payerId });

            // 取得訂單編號
            var orderNumber = executedPayment.transactions[0].invoice_number;

            // 付款成功
            if (executedPayment.state.ToLower() == "approved")
            {
                _orderService.UpdateStatus(orderNumber, Common.OrderStatusEnum.Payment);
            }
            else // 例外狀況: 付款失敗
            {
                _orderService.UpdateStatus(orderNumber, Common.OrderStatusEnum.Failed);
            }

            return RedirectToAction("Index", "Member");
        }

        [Authorize]
        public ActionResult Pay(int orderId)
        {
            var model = _orderService.GetOrder(orderId);
            return View(model);
        }
    }
}