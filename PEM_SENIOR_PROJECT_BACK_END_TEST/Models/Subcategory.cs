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
        [Required(ErrorMessage = "Title can't be empty!")]
        public string Title  { get; set; }

        [Required(ErrorMessage = "Evaluation can't be empty!")]
        public string Evaluation { get; set; }

        [Required(ErrorMessage = "Medications can't be empty!")]
        public string Medications { get; set; }

        [Required(ErrorMessage = "Management can't be empty!")]
        public string Management { get; set; }

        [Required(ErrorMessage = "Symptoms can't be empty!")]
        public string Symptoms { get; set; }

        [Required(ErrorMessage = "References can't be empty!")]
        public string References { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image Link")]
        [Required(ErrorMessage = "Image Link can't be empty!")]
        public string ImageUrl { get; set; }

        //for linking to Main Kategory
        [Display(Name = "Main Category ID")]
        [Required(ErrorMessage = "Main Category can't be empty!")]
        //primary and foreing key values seem to have a default zero value
        //so the check for required above doesn't seem to trigger
        //so the Range check below works since it doesn't allow zero
        [Range(1,int.MaxValue,ErrorMessage = "Please select a Main Category!")]
        public int MainCategoryId { get; set; }

        [ForeignKey("MainCategoryId")]
        public virtual MainCategory MainCategory { get; set; }
    }
}
