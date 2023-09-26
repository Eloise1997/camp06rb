using FProjectCamping.Models.Services;
using FProjectCamping.Models.ViewModels.Orders;
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

		public ActionResult Pay(int orderId)
		{
			var model = _orderService.GetOrder(orderId);
			return View(model);
		}
	}
}