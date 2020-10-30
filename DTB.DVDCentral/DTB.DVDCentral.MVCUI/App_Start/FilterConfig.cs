using System.Web;
using System.Web.Mvc;

namespace DTB.DVDCentral.MVCUI
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
