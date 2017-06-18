using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjektSemi.Models;
using System.Threading.Tasks;
using System.Net;
using PagedList;
using System.Globalization;


namespace ProjektSemi.Controllers
{
    public class NieruchomosciController : Controller
    {
        // GET: Nieruchomosci


        public ActionResult Index(string sortOrder, string searchString, string currentFilter, int? page)
        {

            ViewBag.CurrentSort = sortOrder;

            //ViewBag.TypeSort = String.IsNullOrEmpty(sortOrder) ? "type_desc" : "";
            ViewBag.NameSort = sortOrder == "namesort" ? "namesort_desc" : "namesort";
            ViewBag.CategorySort = sortOrder == "categorysort" ? "categorysort_desc" : "categorysort";
            ViewBag.AddressSort = sortOrder == "addresssort" ? "addresssort_desc" : "addresssort";
            ViewBag.PriceSort = sortOrder == "pricesort" ? "pricesort_desc" : "pricesort";
            ViewBag.SurfaceSort = sortOrder == "surfacesort" ? "surfacesort_desc" : "surfacesort";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            ViewBag.CurrentFilter = searchString;

            var model = DocumentDBRepository.GetIncompleteItems().ToList();

            if (!String.IsNullOrEmpty(searchString))
            {
                model = model.Where(r =>
                 ((CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.Adres.ToString(), searchString, CompareOptions.IgnoreCase) >= 0) ||
                  (CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.Nazwa, searchString, CompareOptions.IgnoreCase) >= 0) ||
                    (CultureInfo.CurrentCulture.CompareInfo.IndexOf(r.Kategoria.ToString(), searchString, CompareOptions.IgnoreCase) >= 0))).ToList();
            }

            switch (sortOrder)
            {

                case "namesort":
                    model = model.OrderBy(m => m.Nazwa).ToList();
                    break;
                case "namesort_desc":
                    model = model.OrderByDescending(m => m.Nazwa).ToList();
                    break;

                case "categorysort":
                    model = model.OrderBy(m => m.Kategoria).ToList();
                    break;
                case "categorysort_desc":
                    model = model.OrderByDescending(m => m.Kategoria).ToList();
                    break;

                case "addresssort":
                    model = model.OrderBy(m => m.Adres).ToList();
                    break;
                case "addresssort_desc":
                    model = model.OrderByDescending(m => m.Adres).ToList();
                    break;

                case "pricesort":
                    model = model.OrderBy(m => m.Cena).ToList();
                    break;
                case "pricesort_desc":
                    model = model.OrderByDescending(m => m.Cena).ToList();
                    break;

                case "surfacesort":
                    model = model.OrderBy(m => m.Powierzchnia).ToList();
                    break;
                case "surfacesort_desc":
                    model = model.OrderByDescending(m => m.Powierzchnia).ToList();
                    break;
            }
            int pageSize = 5;
            int pageNumber = (page ?? 1);


            return PartialView(model.ToPagedList(pageNumber, pageSize));

        }

        public ActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "Id,Nazwa,Kategoria,Adres,Cena,Powierzchnia,CzySprzedane,Dom,Opis,Completed,Dom,Mieszkanie,Lokal,Magazyn")] Nieruchomosci item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository.CreateItemAsync(item);
                return this.RedirectToAction("Index");
            }
            return this.View(item);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "Id,Nazwa,Kategoria,Adres,Cena,Powierzchnia,CzySprzedane,Opis,Completed,Dom,Mieszkanie,Lokal,Magazyn")] Nieruchomosci item)
        {
            if (ModelState.IsValid)
            {
                await DocumentDBRepository.UpdateItemAsync(item);
                return this.RedirectToAction("Index");
            }
            return this.View(item);
        }
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieruchomosci item = (Nieruchomosci)DocumentDBRepository.GetItem(id);
            if (item == null)
            {
                return this.HttpNotFound();
            }
            if (item.Kategoria == Kategoria.Dom)
            {

                return this.View("DomEdit",item);
            }
            if (item.Kategoria == Kategoria.Mieszkanie)
            {

                return this.View("MieszkanieEdit", item);
            }
            if (item.Kategoria == Kategoria.Lokal)
            {

                return this.View("LokalEdit", item);
            }
            if (item.Kategoria == Kategoria.Magazyn)
            {

                return this.View("MagazynEdit", item);
            }
            else
            {
                return this.View(item);
            }
        }
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Nieruchomosci item = (Nieruchomosci)DocumentDBRepository.GetItem(id);
            if (item == null)
            {
                return this.HttpNotFound();
            }
            return this.View(item);
        }
        [HttpPost, ActionName("Delete")]
        //// To protect against Cross-Site Request Forgery, validate that the anti-forgery token was received and is valid
        //// for more details on preventing see http://go.microsoft.com/fwlink/?LinkID=517254
        [ValidateAntiForgeryToken]
        //// To protect from overposting attacks, please enable the specific properties you want to bind to, for
        //// more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        public async Task<ActionResult> DeleteConfirmed([Bind(Include = "Id")] string id)
        {
            await DocumentDBRepository.DeleteItemAsync(id);
            return this.RedirectToAction("Index");
        }
        public ActionResult Details(string id)
        {
            var item = DocumentDBRepository.GetItem(id);
            if (item.Kategoria == Kategoria.Dom)
            {

                return this.View("DomView",item);
            }
            if (item.Kategoria == Kategoria.Mieszkanie)
            {

                return this.View("MieszkanieView", item);
            }
            if (item.Kategoria == Kategoria.Lokal)
            {

                return this.View("LokalView", item);
            }
            if (item.Kategoria == Kategoria.Magazyn)
            {

                return this.View("MagazynView", item);
            }
            else
            {
                return this.View(item);
            }
        }
    }
}