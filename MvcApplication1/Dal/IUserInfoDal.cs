using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MvcApplication1.Models;

namespace MvcApplication1.Dal
{
    public interface IUserInfoDal
    {
        List<User> GetAll();

        int Add(User user);

        int Edit(User user);
    }
}
