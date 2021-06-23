﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Jdsl.Models
{
    public class Shop
    {
        public int Id { get; set; }
        [Required]
        public float ShopNumber { get; set; }

        public string Facia { get; set; }

        public string Town { get; set; }

        public string PhoneNumber { get; set; }

        public string DeliveryInfo { get; set; }

        public string MapCoordinatesLongitude { get; set; }
        public string MapCoordinatesLatitude { get; set; }
    }
}
