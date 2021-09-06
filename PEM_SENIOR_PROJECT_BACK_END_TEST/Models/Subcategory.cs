using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Models
{
    public class Subcategory
    {
        [Key]
        public int Id { get; set; }//primary key. A sequence is created and incremented by default
        public string Title  { get; set; }
        public string Evaluation { get; set; }
        public string Medications { get; set; }
        public string Management { get; set; }
        public string Symptoms { get; set; }
        public string References { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image Link")]
        public string ImageUrl { get; set; }

        //for linking to Main Kategory
        [Required]
        public int MainCategoryId { get; set; }

        [ForeignKey("MainCategoryId")]
        public virtual MainCategory MainCategory { get; set; }
    }
}
