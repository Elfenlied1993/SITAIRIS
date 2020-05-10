using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BSUIR.BL.Interfaces.Models;
using BSUIR.BL.Interfaces.Models.Items;
using BSUIR.BL.Interfaces.Models.Photos;
using BSUIR.BL.Interfaces.Services;
using Microsoft.AspNetCore.Mvc;

namespace BSUIR.Web.Controllers
{
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;
        private readonly IPhotoService _photoService;
        private readonly List<string> Types = new List<string>();
        private readonly List<string> Colors = new List<string>();
        private readonly List<string> Producers = new List<string>();
        private readonly List<string> Materials = new List<string>();
        private readonly List<string> Countries = new List<string>();
        private Basket _basket;
        public ItemsController(IItemService itemService, IPhotoService photoService, Basket basket)
        {
            _itemService = itemService;
            _photoService = photoService;
            this._basket = basket;
        }
        [HttpPost]
        public async Task<IActionResult> Index(int id, string placeholder)
        {
            var basketItem =await _itemService.GetItemByIdAsync<Item>(id);
            var basketPhotos = await _photoService.GetRelatedPhotosAsync<Photo>(basketItem.Id);
            basketItem.Link = basketPhotos.ToList().ElementAt(0).Link;
            _basket.Items.Add(basketItem);
            _basket.Price += basketItem.Price;
            var items = await _itemService.GetItemsAsync<Item>();
            var result = items.Where(x => x.CategoryId == basketItem.CategoryId).ToList();
            foreach (var item in result)
            {
                var photos = await _photoService.GetRelatedPhotosAsync<Photo>(item.Id);

                item.Link = photos.ToList().ElementAt(0).Link;
                if (!Types.Contains(item.Type))
                    Types.Add(item.Type);
                if (!Colors.Contains(item.Color))
                    Colors.Add(item.Color);
                if (!Producers.Contains(item.Producer))
                    Producers.Add(item.Producer);
                if (!Materials.Contains(item.Material))
                    Materials.Add(item.Material);
                if (!Countries.Contains(item.Country))
                    Countries.Add(item.Country);
            }

            ViewData["Types"] = Types;
            ViewData["Colors"] = Colors;
            ViewData["Producers"] = Producers;
            ViewData["Materials"] = Materials;
            ViewData["Countries"] = Countries;
            return View(result);
        } 
        public async Task<IActionResult> Index(int id)
        {
            var items = await _itemService.GetItemsAsync<Item>();
            var result = items.Where(x => x.CategoryId == id).ToList();
            foreach (var item in result)
            {
                var photos = await _photoService.GetRelatedPhotosAsync<Photo>(item.Id);

                item.Link = photos.ToList().ElementAt(0).Link;
                if (!Types.Contains(item.Type))
                    Types.Add(item.Type);
                if (!Colors.Contains(item.Color))
                    Colors.Add(item.Color);
                if (!Producers.Contains(item.Producer))
                    Producers.Add(item.Producer);
                if (!Materials.Contains(item.Material))
                    Materials.Add(item.Material);
                if (!Countries.Contains(item.Country))
                    Countries.Add(item.Country);    
            }

            ViewData["Types"] = Types;
            ViewData["Colors"] = Colors;
            ViewData["Producers"] = Producers;
            ViewData["Materials"] = Materials;
            ViewData["Countries"] = Countries;
            return View(result);
        }
        public async Task<IActionResult> Details(int id)
        {
            var item = await _itemService.GetItemByIdAsync<Item>(id);
            var photos = await _photoService.GetRelatedPhotosAsync<Photo>(id);
            ViewData["Photos"] = photos;
            return View(item);
        }
    }
}