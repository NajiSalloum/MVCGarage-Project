using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCGarage2.DataAccessLayer;
using MVCGarage2.Models;
using PagedList;

namespace MVCGarage2.Controllers
{
    public class VehiclesController : Controller
    {
        private StorageContext db = new StorageContext();

        // GET: Vehicles
        public ActionResult Overview(VehicleType? Type, Color? Color, FuelType? FuelType, string Regnr = "", string Brand = "", string SortOn = "", bool Ascending = true, int page = 1)
        {
            var vehiclesTemp = db.Vehicles.Where(v => (Regnr == "" || v.Regnr.Contains(Regnr)) &&
                                                     (Type == null || v.Type == Type.ToString()) &&
                                                     (Color == null || v.Color == Color.ToString()) &&
                                                     (FuelType == null || v.FuelType == FuelType.ToString()) &&
                                                     (Brand.Trim() == "" || v.Brand.Contains(Brand.Trim())));

            switch (SortOn)
            {
                case "Regnr":
                    if (Ascending)
                        vehiclesTemp = vehiclesTemp.OrderBy(v => v.Regnr);
                    else
                        vehiclesTemp = vehiclesTemp.OrderByDescending(v => v.Regnr);
                    break;
                case "Type":
                    if (Ascending)
                        vehiclesTemp = vehiclesTemp.OrderBy(v => v.Type);
                    else
                        vehiclesTemp = vehiclesTemp.OrderByDescending(v => v.Type);
                    break;
                case "Color":
                    if (Ascending)
                        vehiclesTemp = vehiclesTemp.OrderBy(v => v.Color);
                    else
                        vehiclesTemp = vehiclesTemp.OrderByDescending(v => v.Color);
                    break;
                case "ParkedTime":
                    if (Ascending)
                        vehiclesTemp = vehiclesTemp.OrderBy(v => v.ParkedTime);
                    else
                        vehiclesTemp = vehiclesTemp.OrderByDescending(v => v.ParkedTime);
                    break;
                default:
                    break;
            }

            List<VehicleOverview> vehicles = new List<VehicleOverview>();
            foreach (var v in vehiclesTemp.ToList())
            {
                vehicles.Add(new VehicleOverview(v.Id, v.Regnr, v.Type, v.Color, v.ParkedTime));
            }

            var queryDictionary = new RouteValueDictionary();
            if (Regnr != "")
                queryDictionary.Add("Regnr", Regnr);
            if (Type != null)
                queryDictionary.Add("Type", Type);
            if (Color != null)
                queryDictionary.Add("Color", Color);
            if (Brand != "")
                queryDictionary.Add("Brand", Brand);
            if (FuelType != null)
                queryDictionary.Add("FuelType", FuelType);

            queryDictionary.Add("SortOn", "");
            queryDictionary.Add("Ascending", true);

            var vehiclesOverview = new VehiclesOverview(vehicles.ToPagedList(Math.Max(page, 1), 10), SortOn, Ascending, queryDictionary);

            return View(vehiclesOverview);
        }

        // GET: Vehicles/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Overview");
            }
            ParkedVehicle vehicle = db.Vehicles.Find(id);
            if (vehicle == null)
            {
                return RedirectToAction("Overview");
                //return HttpNotFound();
            }
            return View(vehicle);
        }

        // GET: Vehicles/Create
        public ActionResult Register()
        {
            return View(new VehicleRegister());
        }

        public JsonResult UniqueRegnr(string DataName, string text)
        {
            if (DataName == "Regnr")
            {
                var data = db.Vehicles.Where(v => v.Regnr.Equals(text.Trim(), StringComparison.InvariantCultureIgnoreCase)).Select(v => new { text = v.Regnr }).ToList();
                if (data != null)
                    return Json(data);
            }
            Response.StatusCode = (int)HttpStatusCode.BadRequest;
            return null;
        }
        // POST: Vehicles/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Park(string Regnr, VehicleType Type, Color Color, string Brand, int NrofWheels, double Length, FuelType FuelType)
        {
            if (db.Vehicles.Any(v => v.Regnr == Regnr))
            {
                return RedirectToAction("Register");
            }

            ParkedVehicle vehicle = new ParkedVehicle()
            {
                Regnr = Regnr.ToUpper(),
                Type = Type.ToString(),
                Color = Color.ToString(),
                Brand = Brand,
                NrofWheels = NrofWheels,
                Length = Length,
                FuelType = FuelType.ToString(),
                ParkedTime = DateTime.Now
            };

            if (ModelState.IsValid)
            {
                db.Vehicles.Add(vehicle);
                db.SaveChanges();
            }

            return View(vehicle);
        }

        // GET: Vehicles/Edit/5
        public ActionResult Search()
        {
            return View(new VehicleSearch());
        }

        public JsonResult Autocomplete(string DataName, string text)
        {
            if (DataName == "Regnr")
            {
                var data = db.Vehicles.Where(v => v.Regnr.Substring(0, text.Trim().Length).Equals(text.Trim(), StringComparison.InvariantCultureIgnoreCase)).Select(v => new { text = v.Regnr }).ToList();
                if (data != null)
                    return Json(data);
            }
            else if (DataName == "Brand")
            {
                var data = db.Vehicles.Where(v => v.Brand.Substring(0, text.Trim().Length).Equals(text.Trim(), StringComparison.InvariantCultureIgnoreCase)).Select(v => new { text = v.Brand }).ToList();
                if (data != null)
                    return Json(data);
            }
            return null;
        }

        // GET: Vehicles/Delete/5
        public ActionResult CheckOut(int? id)
        {
            if (id == null)
            {
                return RedirectToAction("Overview");
                //return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ParkedVehicle v = db.Vehicles.Find(id);
            if (v == null)
            {
                return RedirectToAction("Overview");
            }
            VehicleCheckOut vehicle = new VehicleCheckOut(v.Id, v.Regnr, v.ParkedTime, DateTime.Now);
            if (vehicle == null)
            {
                return HttpNotFound();
            }
            return View(vehicle);
        }

        // POST: Vehicles/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Receipt(int id)
        {
            ParkedVehicle v = db.Vehicles.Find(id);
            if (v == null)
            {
                return HttpNotFound();
            }
            db.Vehicles.Remove(v);
            db.SaveChanges();

            VehicleReceipt info = new VehicleReceipt(v.Id, v.Regnr, v.Type, v.ParkedTime, DateTime.Now);
            return View(info);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
