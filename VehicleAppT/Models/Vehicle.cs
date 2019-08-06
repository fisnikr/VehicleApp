using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace VehicleAppT.Models
{
    public class Vehicle
    {
        [Key]
        public int Id { get; set; }
        public string LicensePNr { get; set; }
        public string VinMark { get; set; }
        public int MID { get; set; }

        public virtual VModel VModel { get; set; }
    }
}
