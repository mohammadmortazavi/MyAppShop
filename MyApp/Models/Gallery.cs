using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class Gallery
    {
        [Key]
        public int Id { get; set; }

        public int ProductId { get; set; }
        [Display(Name = "تصاویر")]
        [MaxLength(90, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Img { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Products { get; set; }
    }
}