using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class User :BaseEnitiy
    {
        public int fkRole { get; set; }
        public string Name { get; set; }
        public string Account { get; set; }
        public long PhoneNumber { get; set; }
        public string Password { get; set; }
        public long CreateTime { get; set; }
        public bool IsActive { get; set; }
    }
}