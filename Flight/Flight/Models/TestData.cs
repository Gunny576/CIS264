using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class TestData
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string TimeZone { get; set; }
        public bool? canEmail { get; set; }
        List<string> UserData { get; set; }
    }
}