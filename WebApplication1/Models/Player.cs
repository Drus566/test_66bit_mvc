using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Player
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }
        [Required]
        public string Surname { get; set; }
        [Required]
        public Gender Gender { get; set; }
        [Required]
        [RegularExpression(@"\d{2}/\d{2}/\d{4}", ErrorMessage = "Incorrect data")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public string Birth { get; set; }
        [Required]
        [Display(Name = "Team")]
        public int TeamId { get; set; }
        public Team Team { get; set; }
        [Required]
        public Country Country { get; set; }
    }

    public enum Gender
    {
        Male,
        Female
    }

    public enum Country
    {
        Russia,
        USA,
        Italy
    }
}
