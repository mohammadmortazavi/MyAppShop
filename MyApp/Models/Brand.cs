﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;


namespace MyApp.Models
{
    public class Brand
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام برند")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Name { get; set; }
        [Display(Name = "تصویر برند")]
        [MaxLength(30, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Image { get; set; }
    }
}