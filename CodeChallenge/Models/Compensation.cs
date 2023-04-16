using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CodeChallenge.Models
{
    public class Compensation
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

       // [ForeignKey("Employee")]
        
        public string EmployeeId { get; set; }
        //[ForeignKey("EmployeeId")]
        public Employee Employee { get; set;  }
        
        public int Salary { get; set; }
        public DateTime EffectiveDate { get; set; }
      
    }
}