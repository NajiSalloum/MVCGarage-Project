using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;

namespace MVCGarage2.DataAccessLayer
{
    public class StorageContext : DbContext
    {
        public StorageContext() : base("Exercise12_NAR")
        {
        }
        public DbSet<Models.ParkedVehicle> Vehicles { get; set; }
    }
}