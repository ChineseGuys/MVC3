using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApplication1.Dal
{
   public interface IRoleInfoDal
    {
        List<Role> GetAll();

        int Add(Role role);

        int Edit(Role role);
    }
}
