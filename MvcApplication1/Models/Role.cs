using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Role
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public int IsActive { get; set; }
    }
}