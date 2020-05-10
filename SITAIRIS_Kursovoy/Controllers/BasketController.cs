using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSUIR.BL.Interfaces.Models;
using BSUIR.BL.Interfaces.Models.DeliveryAddresses;
using BSUIR.BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace BSUIR.Web.Controllers
{
    public class BasketController : Controller
    {
        private Basket _basket;
        private readonly IDeliveryAddressService _deliveryAddressService;

        public BasketController(Basket basket, IDeliveryAddressService deliveryAddressService)
        {
            _basket = basket;
            _deliveryAddressService = deliveryAddressService;
        }

        public IActionResult Index()
        {
            return View();
        }
        public async  Task<IActionResult> Address()
        {
            var coordinates = new List<Marker>();
            foreach (var address in await _deliveryAddressService.GetDeliveryAddressesAsync<DeliveryAddress>())
            {
                var coordinatesJson = JsonConvert.DeserializeObject<Coordinates>(address.Coordinates);
                coordinates.Add(new Marker()
                {
                    Address = address.Street + ", " + address.House,
                    AddressId = address.Id,
                    Lat = coordinatesJson.Lat,
                    Lng = coordinatesJson.Lng
                });
            }
            ViewData["DeliveryAddresses"] = await _deliveryAddressService.GetDeliveryAddressesAsync<DeliveryAddress>();
            return View(coordinates);
        }

        public async Task<IActionResult> ChooseAddress(int id, bool isdelivery)
        {
            _basket.IsDelivery = isdelivery;
            _basket.AddressId = id;
            var coordinates = new List<Marker>();
            foreach (var address in await _deliveryAddressService.GetDeliveryAddressesAsync<DeliveryAddress>())
            {
                var coordinatesJson = JsonConvert.DeserializeObject<Coordinates>(address.Coordinates);
                coordinates.Add(new Marker()
                {
                    Address = address.Street + ", " + address.House,
                    AddressId = address.Id,
                    Lat = coordinatesJson.Lat,
                    Lng = coordinatesJson.Lng
                });
            }
            ViewData["DeliveryAddresses"] = await _deliveryAddressService.GetDeliveryAddressesAsync<DeliveryAddress>();
            return View("Address",coordinates);
        }
        public IActionResult RemoveItem(int id)
        {
            var deletedItem = _basket.Items.FirstOrDefault(x => x.Id == id);
            _basket.Items.Remove(deletedItem);
            _basket.Price -= deletedItem.Price;
            return View("Index");
        }
        public IActionResult Confirm()
        {
            return View();
        }
    }
}