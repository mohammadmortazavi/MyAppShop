using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class GenderGategory
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نوع پوشاک")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]

        public string Name { get; set; }
        [Display(Name = "تصویر بنر")]
        [MaxLength(100, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Img { get; set; }
    }
}