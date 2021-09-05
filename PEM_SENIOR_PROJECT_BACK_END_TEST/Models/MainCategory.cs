/*
 This class represents a database table with 2 fields currently: Id, and katerogy name
 this is a first-code approach to designing the database with C# and entity framwork kor
this approch is used since there's not a database in place

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;//for DisplayName

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Models
{
    public class MainCategory
    {
       // [System.ComponentModel.DataAnnotations.Key]
        [Key]
        public int Id { get; set; }//primary key. A sequence is created and incremented by default

        [DisplayName("Category Name")]
        public string CategoryName { get; set; }

        [DataType(DataType.ImageUrl)]
        [Display(Name = "Image Link")]//this makes this field Image Link instead of ImageUrl when used in html
        public string ImageUrl { get; set; }
    }
}
