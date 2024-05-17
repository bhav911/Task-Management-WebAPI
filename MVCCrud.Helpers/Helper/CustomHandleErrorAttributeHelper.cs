using System.Web.Mvc;

public class CustomHandleErrorAttribute : HandleErrorAttribute
{
    public override void OnException(ExceptionContext filterContext)
    {
        base.OnException(filterContext);

        if (!filterContext.ExceptionHandled)
        {
            filterContext.ExceptionHandled = true;

            // Redirect to custom error page
            filterContext.Result = new ViewResult
            {
                ViewName = "Error",
                ViewData = new ViewDataDictionary<HandleErrorInfo>(new HandleErrorInfo(filterContext.Exception, filterContext.RouteData.Values["controller"].ToString(), filterContext.RouteData.Values["action"].ToString())),
                TempData = filterContext.Controller.TempData
            };
        }
    }
}