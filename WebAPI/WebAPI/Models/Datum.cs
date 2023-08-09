using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAPI.Models
{
    public class Datum
    {
        public string countrycode { get; set; }
        public string date { get; set; }
        public string cases { get; set; }
        public string deaths { get; set; }
        public string recovered { get; set; }
    }
}