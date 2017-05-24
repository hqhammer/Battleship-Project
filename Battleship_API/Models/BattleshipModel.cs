using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BattleshipClass;
using System.ComponentModel.DataAnnotations;

namespace Battleship_API.Models
{
    public enum Input
    {
        MakeMove,
        Reset,
        PlaceBoat
    }

    public class BattleshipModel
    {
        [Required]       
        public int Index { get; set; }

        public int Row { get; set; }
        public int Col { get; set; }

        [Required]
        public Input input { get; set; }

        public int ID { get; set; }
        public int BoatSize { get; set; }
        public bool Direction { get; set; }
    }
   
}