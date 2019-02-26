using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyApp.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }

        public int RoleId { get; set; }

        [Display(Name = "شماره همراه")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(11, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string Mobile { get; set; }

        [Display(Name = "کلمه عبور")]
        [Required(ErrorMessage = "مقداری برای {0} قرار دهید")]
        [MaxLength(80, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "کد تایید")]
        [MaxLength(6, ErrorMessage = "نباید بیشتر از {1} کارکتر باشد")]
        public string CodeNumber { get; set; }

        [Display(Name = "فعال/غیرفعال")]
        public bool IsActive { get; set; }



        [ForeignKey("RoleId")]
        public virtual Role Roles { get; set; }

        public virtual ICollection<UserAddress> UserAddresses { get; set; }

    }
}