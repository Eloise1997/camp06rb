namespace FProjectCampingBackend.Models.EFModels
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Data.Entity.Spatial;

	public partial class News
	{
		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public News()
		{
			NewsImages = new HashSet<NewsImage>();
		}

		public int Id { get; set; }

		[Required]
		[StringLength(50)]
		public string Title { get; set; }

		[Required]
		[StringLength(3000)]
		public string Description { get; set; }

		public DateTime CreatedTime { get; set; }

		public DateTime ModifiedTime { get; set; }

		[System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
		public virtual ICollection<NewsImage> NewsImages { get; set; }
	}
}