using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapProject12.Models;
using MapProject12.Repository;
using Microsoft.AspNet.Identity;

namespace MapProject12.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserMasterRepository _repository;

        /// <summary>
        /// Constructor of the class, which creates the DI
        /// </summary>
        /// <param name="repository">Object of the DI unity repository</param>
        public HomeController(IUserMasterRepository repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// Action method with get type to go on a view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// Action method with get type to go on a view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /// <summary>
        /// Action method with get type to go on a view
        /// </summary>
        /// <returns>View</returns>
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /// <summary>
        /// Method gets all visited places by user and creates and objects of the markers using temp ViewBag
        /// </summary>
        /// <returns>View of the map</returns>
        public ActionResult Map()
        {
            string id = User.Identity.GetUserId();
            var list = new List<TempUserModel>();
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                list.AddRange(from i in context.TravelInfos
                    where i.UserId == id
                    select new TempUserModel()
                    {
                        Comment = i.Comment,
                        CityName = i.VisitedCity.CityName,
                        Latitude = i.VisitedCity.Latitude,
                        Longitude = i.VisitedCity.Longitude
                    });
                ViewBag.list = list;
            }
            return View();
        }

        /// <summary>
        /// Getting the visited city which is choosen on the map and sending data to save in database
        /// </summary>
        /// <param name="travelObj">Object, that has data of visit: city, coordinates and comment</param>
        /// <returns>Redirection to another View</returns>
        [HttpPost]
        public ActionResult MapData(TempUserModel travelObj)
        {
            if (ModelState.IsValid)
            {
                _repository.AddCity(travelObj);
                _repository.SaveTravelInfo(travelObj, User.Identity.GetUserId());
            }
            return RedirectToAction("Map");
        }

        /// <summary>
        /// Returns the list of visited cities to show on the map
        /// </summary>
        /// <returns>View with the list of visits</returns>
        public ActionResult ShowVisits()
        {            
            return View(_repository.ShowList());
        }

        /// <summary>
        /// Creates the data of the visit to edit
        /// </summary>
        /// <param name="id">Id of the user, who wants to do edit</param>
        /// <returns></returns>
        public ActionResult EditTravel(int id)
        {
            var entry = _repository.TravelInfos.First(o => o.TravelId == id);
            var city = _repository.Cities.First(o => o.Id == entry.CityId);
            var model = new EditModel()
            {
                Id = entry.TravelId,
                Comment = entry.Comment,
                CityName = city.CityName,
                Latitude = city.Latitude,
                Longitude = city.Longitude,
            };
            return View(model);
        }

        /// <summary>
        /// Editing of the user data visit and adding the photo of the visit if wanted
        /// </summary>
        /// <param name="edit">Temp object data of the visit, which user want to edit</param>
        /// <param name="Picture">File of the visit</param>
        /// <returns>savind data and returning View</returns>
        [HttpPost]
        public ActionResult EditTravel(EditModel edit, HttpPostedFileBase picture)
        {
            if (ModelState.IsValid)
            {
                _repository.Edit(edit, User.Identity.GetUserId());
            }

            if(picture == null) return View("Index");
            var filename = Path.GetFileName(picture.FileName);
            if (filename == null)
                return RedirectToAction("EditTravel", "Home");
            string path = Path.GetFileName(picture.FileName);
            path = "/Images/" + path;
            picture.SaveAs(Server.MapPath(path));
            var photo = new Photo
            {
                TravelId = edit.Id,
                Picture = path
            };
            _repository.AddPhoto(photo);
               
            return View("Index");
        }

        /// <summary>
        /// Deleting the visit from the database
        /// </summary>
        /// <param name="id"></param>
        /// <returns>View</returns>
        public ActionResult Delete(int id)
        {
            var visit = _repository.DeleteVisit(id);
            if (visit != null)
            {
                TempData["message"] = "Visit was deleted";
            }
            return RedirectToAction("Index", "Home");
        }

        /// <summary>
        /// Catch the name of the city and returns the View with the wiki search. Send city name 
        /// using temp parameter
        /// </summary>
        /// <param name="City">Name of the city, we are searching</param>
        /// <returns>View</returns>
        public ActionResult WikiSearch(string City)
        {
            ViewBag.City = City;
            return View();
        }

        /// <summary>
        /// Creates a list of the photos of the veisit of the current user
        /// </summary>
        /// <returns>View with sended list of photos</returns>
        public ActionResult PictureGallery()
        {
            var list = _repository.PhotoGallery(User.Identity.GetUserId());
            ViewBag.Count = list.Count;
            return View(list);
        }
    }
}