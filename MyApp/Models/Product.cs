using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace MyApp.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public int GenderCatId { get; set; }
        public int ProductCatId { get; set; }
        public int BrnadId { get; set; }
        [Display(Name = "نام محصول ")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(100, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Name { get; set; }
        [Display(Name = "تصویر")]
        [MaxLength(30, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Img { get; set; }
        [Display(Name = "توضیحات محصول")]
        [DataType(DataType.MultilineText)]
        [AllowHtml]
        public string Des { get; set; }
        [Display(Name = "قیمت فروش")]
        public int SalePrice { get; set; }
        [Display(Name = "تعداد بازدید")]
        public int NumberSeen { get; set; }
        [Display(Name = "تعداد فروش")]
        public int NumberSale { get; set; }
    }
}