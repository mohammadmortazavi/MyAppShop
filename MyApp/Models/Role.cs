using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MyApp.Models
{
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Display(Name = "نام نقش ")]
        [Required(ErrorMessage ="مقداری برای {0} قرار دهید")]
        [MaxLength(20,ErrorMessage ="نباید بیشتر از {1} کارکتر باشد")]
        public string RoleName { get; set; }

        [Display(Name = " عنوان نقش")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(30, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string RoleTitel { get; set; }
    }
}