using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Battleship_API.Models
{
    public class BoatModel
    {
        [Required]
        public int ID { get; set; }
        [Required]
        public int BoatSize { get; set; }
        [Required]
        public bool Direction { get; set; }
    }
}