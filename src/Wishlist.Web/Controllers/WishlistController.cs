
using System;
using System.IO;
using System.Threading.Tasks;
using System.Web.Mvc;
using Wishlist.Models;
using Wishlist.Services;
using Wishlist.Web.Models;

namespace Wishlist.Web.Controllers
{
    public class WishlistController : BaseController
    {
        private readonly ItemsService _itemsService;

        public WishlistController(ItemsService itemsService)
        {
            _itemsService = itemsService;
        }

        // GET: Wishlist
        public async Task<ActionResult> Index()
        {
            var model = await _itemsService.Get(LoggedUserId);
            model.ForEach(i => i.ImageUrl = Url.Content($"~/uploads/{i.ImageUrl}"));
            return View(model);
        }

        public ActionResult Create()
        {
            return View(new ItemViewModel());
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(ItemViewModel model)
        {
            if (ModelState.IsValid)
            {
                string fileName = string.Empty;
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.ImageFile.FileName)}";
                    model.ImageFile.SaveAs(Server.MapPath($"~/uploads/{fileName}"));
                }
                var item = new Item()
                {
                    BuyUrl = model.BuyUrl,
                    ImageUrl = fileName,
                    Name = model.Name,
                    UserId = LoggedUserId,
                    IsPurchased = model.IsPurchased
                };
                await _itemsService.Add(item);
                return RedirectToAction("index");
            }
            return View(model);
        }

        public async Task<ActionResult> Edit(int id)
        {
            var item = await _itemsService.Get(id, LoggedUserId);
            if (item == null)
            {
                return HttpNotFound();
            }
            var model = new ItemViewModel()
            {
                BuyUrl = item.BuyUrl,
                IsPurchased = item.IsPurchased,
                Name = item.Name
            };
            return View(model);
        }

        [HttpPost, ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit(int id, ItemViewModel model)
        {
            var item = await _itemsService.Get(id, LoggedUserId);
            if (item == null)
            {
                return HttpNotFound();
            }
            if (ModelState.IsValid)
            {
                if (model.ImageFile != null && model.ImageFile.ContentLength > 0)
                {
                    var fileName = $"{Guid.NewGuid().ToString()}{Path.GetExtension(model.ImageFile.FileName)}";
                    model.ImageFile.SaveAs(Server.MapPath($"~/uploads/{fileName}"));
                    item.ImageUrl = fileName;
                }

                item.BuyUrl = model.BuyUrl;
                item.Name = model.Name;
                item.UserId = LoggedUserId;
                item.IsPurchased = model.IsPurchased;

                await _itemsService.Edit(item);
                return RedirectToAction("index");
            }
            return View(model);
        }

        public async Task<ActionResult> Browse(string id)
        {
            var model = await _itemsService.Get(id);
            model.ForEach(i => i.ImageUrl = Url.Content($"~/uploads/{i.ImageUrl}"));
            return View(model);
        }

        public ActionResult Share()
        {
            ViewBag.Link = $"{Request.Url.GetLeftPart(UriPartial.Authority)}{Url.Content("~/")}wishlist/browse/{LoggedUserId}";
            return View();
        }

        [HttpPost]
        public async Task<JsonResult> Delete(int id)
        {
            try
            {
                var item = new Item() { Id = id };
                await _itemsService.Delete(item);
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch
            {
                throw;
            }
        }
    }
}