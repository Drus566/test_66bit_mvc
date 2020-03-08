using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace WebApplication1.Models
{
    public class Team
    {
        [Key]
        public int TeamId { get; set; }
        [Required]
        public string Name { get; set; }
        public List<Player> Players { get; set; }
    }
}
