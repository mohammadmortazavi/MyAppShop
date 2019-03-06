using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class SocialNetwork
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام شبکه")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string Name { get; set; }
        [Display(Name = "آیکن")]
        public string Img { get; set; }
        [Display(Name = "نمایش /عدم نمایش")]
        public bool IsShow { get; set; }
        [Display(Name = "لینک کانال")]
        public string Link { get; set; }
    }
}