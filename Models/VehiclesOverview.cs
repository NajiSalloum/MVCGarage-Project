using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace MVCGarage2.Models
{
    public class VehiclesOverview
    {
        public VehiclesOverview(IPagedList<VehicleOverview> vehicles, string sortOn, bool ascending, RouteValueDictionary queryDictionary)
        {
            Vehicles = vehicles;
            SortOn = sortOn;
            Ascending = ascending;
            QueryDictionary = queryDictionary;
        }

        public IPagedList<VehicleOverview> Vehicles { get; set; }
        public string SortOn { get; set; }
        public bool Ascending { get; set; }
        public RouteValueDictionary QueryDictionary { get; set; }
    }
}
