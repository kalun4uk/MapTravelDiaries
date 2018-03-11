using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapProject12.Models;

namespace MapProject12.Repository
{
    public class UserMasterRepository : IUserMasterRepository
    {

        private bool _disposed;
        readonly ApplicationDbContext _context = new ApplicationDbContext();

        public IEnumerable<ApplicationUser> Users => _context.Users;
        public IEnumerable<TravelInfo> TravelInfos => _context.TravelInfos;
        public IEnumerable<City> Cities => _context.Cities;
        public IEnumerable<Photo> Photos => _context.Photos;

        public void SaveTravelInfo(TempUserModel travelObj, string userId)
        {
            var info = new TravelInfo()
            {
                Comment = travelObj.Comment,
                UserId = userId,
                CityId = _context.Cities.First(o=> o.CityName == travelObj.CityName).Id,
            };
            _context.TravelInfos.Add(info);
            _context.SaveChanges();
        }

        public void AddCity(TempUserModel temp)
        {
            City city;
            if (VerificationCity(temp.CityName))
            {
                city = new City()
                {
                    CityName = temp.CityName,
                    Latitude = temp.Latitude,
                    Longitude = temp.Longitude
                };
                _context.Cities.Add(city);
                _context.SaveChanges();
            }
        }

        public TravelInfo DeleteVisit(int visitId)
        {
            var dbEntry = _context.TravelInfos.Find(visitId);
            if (dbEntry == null)
                return null;
            _context.TravelInfos.Remove(dbEntry);
            _context.SaveChanges();
            return dbEntry;
        }

        public void Edit(EditModel edit, string user)
        {
            if (edit.Id == 0)
            {
                var tempTravel = new TravelInfo()
                {
                    Comment = edit.Comment,
                    CityId = _context.Cities.First(o => o.CityName == edit.CityName).Id,
                    UserId = user,
                };
                _context.TravelInfos.Add(tempTravel);
            }
            var dbEntry = _context.TravelInfos.Find(edit.Id);
            if (dbEntry != null)
            {
                dbEntry.VisitedCity = _context.Cities.First(o => o.CityName == edit.CityName);
                dbEntry.Comment = edit.Comment;
            }
            _context.SaveChanges();
        }

        public bool VerificationCity(string name)
        {
            return _context.Cities.Where(p => p.CityName == name).ToList().Count == 0;
        }

        public void AddPhoto(Photo photo)
        {
            _context.Photos.Add(photo);
            _context.SaveChanges();
        }

        public List<EditModel> ShowList()
        {
            var list = new List<EditModel>();
            foreach (var visit in _context.TravelInfos)
            {
                var city = _context.Cities.FirstOrDefault(o=>o.Id == visit.CityId);
                list.Add(new EditModel()
                {
                    Id = visit.TravelId,
                    Comment = visit.Comment,
                    CityName = city.CityName,
                    Latitude = city.Latitude,
                    Longitude = city.Longitude
                });
            }
            return list;
        }

        public List<TempPhoto> PhotoGallery(string id)
        {
            var temp = new List<TempPhoto>();
            int iterator = 2;
            var travel = _context.TravelInfos.Where(i => i.UserId == id).Select(i => i.TravelId);
            foreach (var i in travel)
            {
                foreach (var t in _context.Photos.Where(o=> o.TravelId == i).Select(a => a.Picture))
                {
                    try
                    {
                        if (t != null)
                        {
                            temp.Add(new TempPhoto()
                            {
                                Counter = iterator,
                                Picture = t
                            });
                            iterator++;
                        }
                    }
                    catch (NullReferenceException)
                    {
                    }
                }
            }
            return temp;
        }

        public virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}