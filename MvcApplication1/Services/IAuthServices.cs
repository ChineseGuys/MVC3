using MvcApplication1.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MvcApplication1.Services
{
    public interface IAuthServices
    {
        List<Auth> GetAll();

        void Insert(Auth auth);
    }
}
