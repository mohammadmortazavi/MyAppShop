using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class Size
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "سایز")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string NameSize { get; set; }

        public virtual ICollection<SizeColor> SizeColors { get; set; }

    }
}