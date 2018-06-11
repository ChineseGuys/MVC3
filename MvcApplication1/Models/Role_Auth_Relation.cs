using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MvcApplication1.Models
{
    public class Role_Auth_Relation : BaseEnitiy
    {
        public int RoleID { get; set; }
        public int AuthID { get; set; }
    }
}