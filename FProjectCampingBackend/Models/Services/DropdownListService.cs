using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FProjectCampingBackend.Models.Services
{
    public class DropdownListService
    {
        public SelectList GetEnabledDropdownList()
        {
            var items = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "請選擇" },
            new SelectListItem { Value = "true", Text = "是" },
            new SelectListItem { Value = "false", Text = "否" },
        };
            return new SelectList(items, "Value", "Text");
        }

        public SelectList GetIsConfirmedDropdownList()
        {
            var items = new List<SelectListItem>
        {
            new SelectListItem { Value = "", Text = "請選擇" },
            new SelectListItem { Value = "true", Text = "是" },
            new SelectListItem { Value = "false", Text = "否" },
        };
            return new SelectList(items, "Value", "Text");
        }
    }
}