using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Role : BaseEnitiy
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}