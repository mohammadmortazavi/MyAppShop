using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;



namespace MyApp.Models
{
    public class RegisterViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(11, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = " تکرار کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage ="کلمه عبور همخانی ندارد")]
        public string RePassword { get; set; }
    }
    public class LoginViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(11, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }


    public class ActiveViewModel
    {
        [Display(Name = "کد تایید")]
        [MaxLength(6, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string CodeNumber { get; set; }
    }

    public class CheckMobileViewModel
    {
        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(11, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string Mobile { get; set; }

    }
    public class ForgetPasswordViewModel
    {
        [Display(Name = "کد تایید")]
        [MaxLength(6, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string CodeNumber { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = " تکرار کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه عبور همخانی ندارد")]
        public string RePassword { get; set; }
    }

    public class ChangePasswordViewModel
    {
        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        public string OldPassword { get; set; }

        [Display(Name = "  جدید کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "جدید تکرار کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "کلمه عبور همخانی ندارد")]
        public string RePassword { get; set; }
    }
}