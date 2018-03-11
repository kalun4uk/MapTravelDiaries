using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapProject12.Models
{
    public class Photo
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        public int TravelId { get; set; }

        public TravelInfo Travel { get; set; }

        public string Picture { get; set; }
    }
}