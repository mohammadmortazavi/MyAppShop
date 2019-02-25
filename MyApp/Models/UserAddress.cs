using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class UserAddress
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(30,ErrorMessage ="نباید بیشتر از {1} کارکتر باشد")]
        public string Ostan { get; set; }

        [Display(Name = "شهرستان")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(50, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string City { get; set; }

        [Display(Name = "استان")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [DataType(DataType.MultilineText)]
        public string Address { get; set; }

        [Display(Name = "کد پستی")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(15, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string PostalCodd { get; set; }



        [ForeignKey("UserId")]
        public virtual User Users { get; set; }
    }
}