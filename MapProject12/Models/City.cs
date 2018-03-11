using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MapProject12.Models
{
    public class City
    {
            [HiddenInput(DisplayValue = false)]
            public int Id { get; set; }

            [Required]
            public string CityName { get; set; }

            [Required]
            public float Latitude { get; set; }

            [Required]
            public float Longitude { get; set; }
    }
}