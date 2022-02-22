using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeExercise_Model.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
        [MaxLength(100)]
        public string? Description { get; set; }
        [Range(0,100)]
        public int? AgeRestriction { get; set; }
        [Required]
        [MaxLength(50)]
        public string Company { get; set; }
        [Required]
        [Range(1,1000)]
        public decimal Price { get; set; }

        public string? ImageUrl { get; set; }


    }
}
