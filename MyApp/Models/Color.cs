using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Color
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام رنگ")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(30, ErrorMessage = "نباید بیشتر از {1} کاراکتر باشد")]
        public string NameColor { get; set; }

        [Display(Name = "نام لاتین رنگ")]
        [Required(ErrorMessage = "مقداری برای {0} وارد نمایید")]
        [MaxLength(30, ErrorMessage = "نباید بیشتر از {1} کاراکتر باشد")]
        public string LatinName { get; set; }

        public virtual ICollection<SizeColor> SizeColors { get; set; }
    }
}