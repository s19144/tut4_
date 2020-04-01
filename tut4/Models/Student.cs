using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace tut4.Models
{
    public class Student
    {
        public string IndexNumber { get; set; }
        [Required(ErrorMessage = "Error")]
        [MaxLength(10)]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
    }
}
