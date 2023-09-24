namespace FProjectCampingBackend.Models.EFModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class NewsImage
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FileName { get; set; }

        public int NewsId { get; set; }

        public int DisplayOrder { get; set; }

        public virtual News News { get; set; }
    }
}
