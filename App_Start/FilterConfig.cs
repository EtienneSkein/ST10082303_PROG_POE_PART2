using System.Web;
using System.Web.Mvc;

namespace ST10082303_PROG_POE_PART2
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }
    }
}
