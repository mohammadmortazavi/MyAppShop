using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class SizeColor
    {
        [Key]
        public int Id { get; set; }
        public int SizeId { get; set; }
        public int ColorId { get; set; }
        public int ProductId { get; set; }

        [ForeignKey("SizeId")]
        public virtual Size Sizes { get; set; }

        [ForeignKey("ColorId")]
        public virtual Color Colors { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Prodcuts { get; set; }




    }
}