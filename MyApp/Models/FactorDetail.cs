using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MyApp.Models
{
    public class FactorDetail
    {
        [Key]
        public int Id { get; set; }
        public int factorId { get; set; }
        public int ProdcutId { get; set; }
        [Display(Name ="تعداد")]
        public int Tedad { get; set; }

        [ForeignKey("factorId")]
        public virtual Factor Factors { get; set; }
        [ForeignKey("ProdcutId")]
        public virtual Product Products{ get; set; }
    }
}