using System;
using System.Collections.Generic;
using System.Web;
using MapProject12.Models;

namespace MapProject12.Repository
{
    public interface IUserMasterRepository: IDisposable
    {
        IEnumerable<ApplicationUser> Users { get; }
        IEnumerable<TravelInfo> TravelInfos { get; }
        IEnumerable<City> Cities { get; }
        IEnumerable<Photo> Photos { get; }

        bool VerificationCity(string name);
        void SaveTravelInfo(TempUserModel travelObj, string userId);
        void AddCity(TempUserModel temp);
        void AddPhoto(Photo photo);
        List<EditModel> ShowList();
        void Edit(EditModel edit, string user);
        TravelInfo DeleteVisit(int visitId);
        List<TempPhoto> PhotoGallery(string id);
    }
}
