using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PinBlockWeb
{
    public class PinBlockRequest
    {
        public string PAN { get; set; }
        public string PIN { get; set; }
        public string ZPK { get; set; }
        public string COMP1 { get; set; }
        public string COMP2 { get; set; }
    }
}