using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FlowersBack.Models
{
    public class FlowerView
    {
        public int FlowerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]

        public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required]
        public decimal Price { get; set; }

        //[DataType(DataType.Date)]//Voy a utilizar l datatimepicker
        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        public string Image { get; set; }


        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observation { get; set; }


    }
}