using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using VehicleAppT.Models;

namespace VehicleAppT.Models
{
    public class VehicleAppTContext : DbContext
    {
        public VehicleAppTContext (DbContextOptions<VehicleAppTContext> options)
            : base(options)
        {
        }

        public DbSet<VehicleAppT.Models.VModel> VModel { get; set; }

        public DbSet<VehicleAppT.Models.Vehicle> Vehicle { get; set; }
    }
}
