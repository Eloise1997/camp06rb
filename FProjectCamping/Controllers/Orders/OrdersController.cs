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

        //public ActionResult GetByMember()
        //{
        //#region Fake Data

        //var model = new List<GetByMemberVm>
        //{
        //	new GetByMemberVm()
        //	{
        //		DisplayNumber = 1,
        //		OrderNumber = "AA0915230001",
        //		OrderTime = "2023/09/15",
        //		PaymentType = "現金",
        //		Price = 36000,
        //		Status = "已完成",
        //		OrderItems = new List<OrderItems>
        //		{
        //			new OrderItems
        //			{
        //				RoomType = "森林雙人房",
        //				CheckInDate = "2023/09/15",
        //				CheckOutDate = "2023/09/17",
        //				Days = 2,
        //				SubTotal = 1800,
        //			},
        //			new OrderItems
        //			{
        //				RoomType = "森林四人房",
        //				CheckInDate = "2023/09/15",
        //				CheckOutDate = "2023/09/17",
        //				Days = 2,
        //				SubTotal = 3500,
        //			}
        //		}
        //	},
        //	new GetByMemberVm()
        //	{
        //		DisplayNumber = 2,
        //		OrderNumber = "AA0915230002",
        //		OrderTime = "2023/09/17",
        //		PaymentType = "現金",
        //		Price = 555,
        //		Status = "已完成",
        //		OrderItems = new List<OrderItems>
        //		{
        //			new OrderItems
        //			{
        //				RoomType = "森林雙人房",
        //				CheckInDate = "2023/09/15",
        //				CheckOutDate = "2023/09/17",
        //				Days = 2,
        //				SubTotal = 1800,
        //			},
        //			new OrderItems
        //			{
        //				RoomType = "森林四人房",
        //				CheckInDate = "2023/09/15",
        //				CheckOutDate = "2023/09/17",
        //				Days = 2,
        //				SubTotal = 3500,
        //			}
        //		}
        //	},
        //	new GetByMemberVm()
        //	{
        //		DisplayNumber = 3,
        //		OrderNumber = "AA0915230003",
        //		OrderTime = "2023/09/19",
        //		PaymentType = "現金",
        //		Price = 3600,
        //		Status = "已完成",
        //		OrderItems = new List<OrderItems>
        //		{
        //			new OrderItems
        //			{
        //				RoomType = "森林雙人房",
        //				CheckInDate = "2023/09/15",
        //				CheckOutDate = "2023/09/17",
        //				Days = 2,
        //				SubTotal = 1800,
        //			},
        //			new OrderItems
        //			{
        //				RoomType = "森林四人房",
        //				CheckInDate = "2023/09/15",
        //				CheckOutDate = "2023/09/17",
        //				Days = 2,
        //				SubTotal = 3500,
        //			}
        //		}
        //	}
        //};

        //#endregion Fake Data

        //return View(model);
        //}

        public ActionResult Pay()
        {
            int orderId = 5;
            var model = _orderService.GetOrder(orderId);
            return View(model);
        }

        public ActionResult Paypal()
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
                invoice_number = new Random().Next(100000).ToString(), // 生成一個隨機的發票號碼
                amount = new Amount { currency = "USD", total = "20.00" }, // 設定金額和貨幣
            }
        },
                redirect_urls = new RedirectUrls
                {
                    return_url = "http://localhost:yourport/Home/ExecutePayment", // 付款完成後返回的URL
                    cancel_url = "http://localhost:yourport/Home/Payment" // 付款取消後返回的URL
                }
            });

            // 取得並傳送PayPal付款頁面的鏈接到前端
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
    }
}