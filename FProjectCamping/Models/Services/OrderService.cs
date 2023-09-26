using FProjectCamping.Models.EFModels;
using FProjectCamping.Models.Respositories;
using FProjectCamping.Models.ViewModels.Carts;
using FProjectCamping.Models.ViewModels.Orders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using static FProjectCamping.MvcApplication;

namespace FProjectCamping.Models.Services
{
	public class OrderService
	{
		private readonly OrderRepository _orderRepository = new OrderRepository();
		private readonly PaymentTypeRepository _paymentTypeRepository = new PaymentTypeRepository();

		public void CreateOrder(string account, CartVm cart)
		{
			// todo : 拉到Repo
			var db = new AppDbContext();
			var memberId = db.Members.First(m => m.Account == account).Id;

			var order = new Order
			{
				MemberId = memberId,
				Name = cart.ContactProfile.Name,
				PhoneNum = cart.ContactProfile.PhoneNum,
				Email = cart.ContactProfile.Email,
				OrderTime = DateTime.Now,
				Coupon = cart.Coupon,
				Status = 1, // todo : 建立enum
				PaymentTypeId = cart.PaymentType,
				TotalPrice = cart.TotalPrice,
			};
			// 新增訂單明細
			foreach (var item in cart.Items)
			{
				var orderItem = new OrderItem
				{
					RoomId = item.RoomId,
					//RoomName = item.RoomName,
					Days = item.Days,
					CheckInDate = Convert.ToDateTime(item.CheckInDate),
					CheckOutDate = Convert.ToDateTime(item.CheckOutDate),
					ExtraBed = item.ExtraBed,
					ExtraBedPrice = item.ExtraBedPrice,
					SubTotal = item.SubTotal
				};
				order.OrderItems.Add(orderItem);
			}
			db.Orders.Add(order);
			db.SaveChanges();
		}

		public PayOrderVm GetOrder(int orderId)
		{
			var order = _orderRepository.GetOrders(orderId);
			var vm = AutoMapperHelper.MapperObj.Map<PayOrderVm>(order);
			vm.PaymentType = _paymentTypeRepository.GetTypeName(order.PaymentTypeId).Name;
			return vm;
		}
	}
}