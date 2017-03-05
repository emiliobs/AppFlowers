using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FlowersBack.Models
{
    public class Flower
    {
        [Key]
        public int FlowerId { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2)]
        [Index("Flower_Description_Index",IsUnique = true)]public string Description { get; set; }

        [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = false)]
        [Required]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Last Purchase")]
        public DateTime? LastPurchase { get; set; }

        public string   Image { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; }

        [DataType(DataType.MultilineText)]
        public string Observation { get; set; }

    }
}