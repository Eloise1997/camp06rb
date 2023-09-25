﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FProjectCampingBackend.Models.ViewModels.Orders
{
	public class OrderVm
	{
		/// <summary>
		/// 訂單編號
		/// </summary>
		[Display(Name = "訂單編號")]
		public string OrderNumber { get; set; }

		/// <summary>
		/// 訂單日期
		/// </summary>
		[Display(Name = "訂單日期")]
		public string OrderTime { get; set; }

		/// <summary>
		/// 付款方式
		/// </summary>
		[Display(Name = "付款方式")]
		public string PaymentType { get; set; }

		/// <summary>
		/// 金額
		/// </summary>
		[Display(Name = "金額")]
		[DisplayFormat(DataFormatString = "{0:#,#}")]
		public int Price { get; set; }

		/// <summary>
		/// 訂單狀態
		/// </summary>
		[Display(Name = "訂單狀態")]
		public string Status { get; set; }

		/// <summary>
		/// 訂購人姓名
		/// </summary>
		[Display(Name = "訂購人姓名")]
		public string Name { get; set; }

		// <summary>
		/// 訂購人電子郵件
		/// </summary>
		[Display(Name = "訂購人電子郵件")]
		public string Email { get; set; }

		// <summary>
		/// 訂購人電話
		/// </summary>
		[Display(Name = "訂購人電話")]
		public string PhoneNum { get; set; }

		public List<OrderItems> OrderItems { get; set; }
	}
}

public class OrderItems
{
	/// <summary>
	/// 房型
	/// </summary>
	[Display(Name = "房型")]
	public string RoomType { get; set; }

	/// <summary>
	/// 房號
	/// </summary>
	[Display(Name = "房號")]
	public int RoomNum { get; set; }

	/// <summary>
	/// 入住日
	/// </summary>
	[Display(Name = "入住日")]
	[DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
	public DateTime CheckInDate { get; set; }

	/// <summary>
	/// 退房日
	/// </summary>
	[Display(Name = "退房日")]
	[DisplayFormat(DataFormatString = "{yyyy/MM/dd}")]
	public DateTime CheckOutDate { get; set; }

	/// <summary>
	/// 天數
	/// </summary>
	[Display(Name = "夜數")]
	public int Days { get; set; }

	/// <summary>
	/// 小計
	/// </summary>
	[Display(Name = "小計")]
	[DisplayFormat(DataFormatString = "{0:#,#}")]
	public int SubTotal { get; set; }
}