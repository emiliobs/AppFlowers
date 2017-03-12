using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace FlowersBack.Models
{    //no me envia esta clase a la bd:
    [NotMapped]
    public class FlowerView  : Flower
    {
        [Display(Name = "Image")]
        public HttpPostedFileBase ImageFile { get; set; }    

    }
}