using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Entity
{
    [Table("T_User")]
    public class User
    {
        [Key]
        public int Id { get; set; }
        public int IsAdmin { get; set; }
        public string Nom { get; set; } = string.Empty;
        public string Prenom { get; set; } = string.Empty;
        [Required]
        public string Email { get; set; } = string.Empty;
        public string Tel { get; set; } = string.Empty;
        public string Img { get; set; } = string.Empty;
        public string MotPasse { get; set; } = string.Empty;
        
    }
}
