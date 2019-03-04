using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace MyApp.Models
{
    public class Setting
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "نام فروشگاه")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Name { get; set; }
        [Display(Name = "توضیحات")]
        public string Des { get; set; }
        [Display(Name = "کلمات کلیدی")]
        public string Key { get; set; }
        [Display(Name = "لوگو")]
        public string Logo { get; set; }
        [Display(Name = "کاربر پیامک")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string UserSms { get; set; }

        [Display(Name = "پسورد پیامک")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string PassSms { get; set; }

        [Display(Name = "فرستنده پیامک")]
        [MaxLength(15, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string SendSms { get; set; }

        [Display(Name = "فعالسازی کاربر پس از ثبت نام")]
        public bool ActiveUser { get; set; }

        [Display(Name = "ارسال پیامک پس از پرداخت")]
        public bool SendUser { get; set; }

        [Display(Name = "ادرس")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }
        [Display(Name = "شماره تلفن ثابت")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Tel { get; set; }
        [Display(Name = "شماره فکس")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Fax { get; set; }

        [Display(Name = "شماره همراه")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]

        public string Mobile { get; set; }

        [Display(Name = "درصد ارزش افزوده")]
        public int TaxPercent { get; set; }

        [Display(Name = "مبلغ کرایه")]
        public int ServicePric { get; set; }
        [Display(Name = "دریافت کرایه تا مبلغ")]
        public int ServiceBetween { get; set; }


    }
}