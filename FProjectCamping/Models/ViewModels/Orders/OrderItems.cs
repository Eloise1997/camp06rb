using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace FProjectCamping.Models.ViewModels.Orders
{
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
		public string CheckInDate { get; set; }

		/// <summary>
		/// 退房日
		/// </summary>
		[Display(Name = "退房日")]
		public string CheckOutDate { get; set; }

		/// <summary>
		/// 天數
		/// </summary>
		[Display(Name = "天數")]
		public int Days { get; set; }

		/// <summary>
		/// 小計
		/// </summary>
		[Display(Name = "小計")]
		public int SubTotal { get; set; }
	}
}