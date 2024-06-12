using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using PetCareSystem.Services.Models.Booking;
using PetCareSystem.Services.Services.Bookings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PetCareSystem.Data.Entites;


namespace PetCareSystem.WebApp.Controllers
{
    public class BookingController : Controller
    {
        [HttpGet]
        public ActionResult Insert()
        {
            using (var context = new DB())
            {
                var services = context.Service.ToList();
                ViewBag.Services = services;
        }
            return View();
        }

        [HttpPost]
        public ActionResult Insert(Booking model, List<int> SelectedServices)
        {
            using (var context = new DB())
            {
                decimal totalPrice = 0;
                int totalServices = 1;


                for (int i = 0; i < SelectedServices.Count; i++)
            {
                    int serviceID = SelectedServices[i];
                    int quantity = serviceQuantities[i];
                    var service = context.Service.FirstOrDefault(s => s.ServiceID == serviceID);
                    if (service != null)
                {
                        totalPrice += service.Price;
                        totalServices += i;
                }
                return BadRequest("Failed to create booking");
            }

                model.TotalPrice = totalPrice;
                model.NumberService = totalServices;

                context.Booking.Add(model);
                context.SaveChanges();
            }
            string message = "Created the booking successfully";
            ViewBag.Message = message;
            return View();
        }



        [HttpGet]
        public ActionResult Search()
        {
            using (var context = new DB())
            {
                var data = context.Booking.ToList();
                return View(data);
                }

            }

        [HttpGet]
        public ActionResult Update(int BookingID)
        {
            using (var context = new DB())
            {
                var data = context.Booking.Include(b => b.Service).FirstOrDefault(x => x.BookingID == BookingID);
                ViewBag.Services = context.Service.ToList(); 
                return View(data);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(int BookingID, List<int> SelectedServices)
        {
            using (var context = new DB())
            {
                var booking = context.Booking.Include(b => b.Service).FirstOrDefault(x => x.BookingID == BookingID);

                decimal totalPrice = 0;
                int totalServices = 1;

                foreach (int serviceID in SelectedServices)
            {
                    var service = context.Service.FirstOrDefault(s => s.ServiceID == serviceID);
                    if (service != null)
                {
                        totalPrice += service.Price;
                        totalServices++;
                    }
                }

                booking.TotalPrice = totalPrice;
                booking.NumberService = totalServices;

                context.Booking.Add(model);
                context.SaveChanges();
                }
            string message = "Updated the booking successfully";
            ViewBag.Message = message;
            }

        public ActionResult Delete()
            {
            return View();
        }

        [HttpPost]
        public ActionResult Delete(int BookingID)
        {
            using (var context = new DB())
            {
                var data = context.Booking.FirstOrDefault(x => x.BookingID == BookingID);
                if (data != null)
                {
                    context.Booking.Remove(data);
                    context.SaveChanges();
            }
                else
                    return View();
            }
        }
    }
}
