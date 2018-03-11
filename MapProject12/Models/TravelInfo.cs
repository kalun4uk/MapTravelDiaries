using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapProject12.Models
{
    public class TravelInfo
    {
        [HiddenInput(DisplayValue = false)]
        [Key]
        public int TravelId { get; set; }

        [DataType(DataType.MultilineText)]
        public string Comment { get; set; }

        public int CityId { get; set; }

        public string UserId { get; set; }

        public ApplicationUser User { get; set; }

        public City VisitedCity { get; set; }

        public List<Photo> Photoses { get; set; }
    }
}