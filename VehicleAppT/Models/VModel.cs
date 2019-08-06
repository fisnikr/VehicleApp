using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleAppT.Models
{
    public class VModel
    {
        [Key]
        public int MID { get; set; }

        public string Model { get; set; }
    }
}
