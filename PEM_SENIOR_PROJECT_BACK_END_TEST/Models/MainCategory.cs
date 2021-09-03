/*
 This class represents a database table with 2 fields currently: Id, and katerogy name
 this is a first-code approach to designing the database with C# and entity framwork kor
thsi approch is used since there's not a database in place

 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace PEM_SENIOR_PROJECT_BACK_END_TEST.Models
{
    public class MainCategory
    {
       // [System.ComponentModel.DataAnnotations.Key]
        [Key]
        public int Id { get; set; }//primary key. A sequence is created and incremented by default
        public string CategoryName { get; set; }
    }
}
