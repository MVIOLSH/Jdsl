using System;
using System.Collections.Generic;
using System.Text;

namespace Jdsl.Models
{
    public class ImgShop
    {
        public int Id { get; set; }
        public string ShopNumber { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string Description { get; set; }
        public int AddAddressId { get; set; }
        public string Url { get; set; }
    }
}
