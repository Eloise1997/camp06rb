
using FProjectCamping.Models.EFModels;
using FProjectCamping.Models.ViewModels;
using FProjectCamping.Models.ViewModels.Rooms;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace FProjectCamping.Controllers.Rooms
{
	public class RoomsController : Controller
	{
		// GET: Rooms
		public ActionResult Roomtype()
		{

			ViewBag.model = 1;

			return View();
		}
		public ActionResult Forestarea()
		{
			return View();
		}
		public ActionResult RiversideDistrict()
		{
			return View();
		}

		public ActionResult RoomsPartial()
		{
			ViewBag.HotProducts = BranchroomPartial();
			return View();
		}

		private List<RoomtypeVM> BranchroomPartial()
		{
			var db = new AppDbContext();
			var branches = db.Rooms
				.Select(c => new RoomtypeVM
				{
					Id = c.Id,
					RoomName = c.RoomName,
					WeekendPrice = c.WeekendPrice,
					WeekdayPrice = c.WeekdayPrice,

				}).ToList();

			foreach (var branch in branches)
			{
				branch.FileName = db.Photos.FirstOrDefault(c => c.RoomTypeId == branch.Id);
			}

			return branches;
		}
	}
}