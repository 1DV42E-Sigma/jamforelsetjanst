using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TownComparisons.Domain
{
    public class SessionHandler
    {
        public object GetSession(string sessionName)
        {
            return System.Web.HttpContext.Current.Session[sessionName];
        }

        public void AddSession(string sessionName, object obj)
        {
            System.Web.HttpContext.Current.Session[sessionName] = obj;
        }
    }
}
