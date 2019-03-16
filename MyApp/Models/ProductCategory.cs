using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class ProductCategory
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام گروه بندی")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]

        public string Name { get; set; }
        [Display(Name = "نمایش / عدم نمایش")]
        public bool IsShow { get; set; }

        public virtual ICollection<Product> Products { get; set; }
    }
}