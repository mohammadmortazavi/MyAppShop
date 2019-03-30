using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class Factor
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }

        [Display(Name = "تاریخ ایجاد")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string DateFactor { get; set; }

        [Display(Name = "شماره سفارش")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string NumberFactor { get; set; }
        [Display(Name = "جمع اقلام")]
        public int SumItem { get; set; }
        [Display(Name = "مبلغ تخفیف")]
        public int CutPrice { get; set; }
        [Display(Name = "مبلغ ارزش افزوده")]
        public int Tax { get; set; }
        [Display(Name = "جمع کل")]
        public int SumPrice { get; set; }
        [Display(Name = "پرداخت/عدم پرداخت")]
        public bool IsPay { get; set; }


        [Display(Name = "شماره پیگیری")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string NumberControl { get; set; }


        [Display(Name = "تاریخ پرداخت")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string DatePay { get; set; }


        [Display(Name = "ساعت چرداخت")]
        [MaxLength(20, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string TimePay { get; set; }


        [ForeignKey("UserId")]
        public  virtual User Usres { get; set; }

        public virtual ICollection<FactorDetail> FactorDetails { get; set; }
    }
}