using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FProjectCampingBackend.Models.Services
{
	public class OrderService
	{
		public SelectList GetStatusDropdownList()
		{
			var items = new List<SelectListItem>
		{
			new SelectListItem { Value = "", Text = "請選擇" },
			new SelectListItem { Value = "Unpaid", Text = "未付款" },
			new SelectListItem { Value = "Paid", Text = "已付款" },
			new SelectListItem { Value = "Cancelled", Text = "已取消" },
			new SelectListItem { Value = "Completed", Text = "已完成" },
		};
			return new SelectList(items, "Value", "Text");
		}
	}
}