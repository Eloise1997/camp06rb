using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FProjectCampingBackend.Models.ViewModels
{
	public class MemberVm
	{
		public string Name { get; set; }
		public string Account { get; set; }
		public bool? Enabled { get; set; }
		public DateTime? FirstTime { get; set; }
		public DateTime? EndTime { get; set; }
        public bool? IsConfirmed { get; set; }
    }
}