using AspNetCoreHero.ToastNotification.Abstractions;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Specialized;

namespace MVCFinalProje.UI.Controllers
{
    public class BaseController : Controller
    {
        protected INotyfService notyfService => HttpContext.RequestServices.GetService(typeof(INotyfService)) as INotyfService;

        protected void SuccessNotfy(string message)
        {
            notyfService.Success(message);
        }

        protected void ErrorNotfy(string message)
        {
            notyfService.Error(message);
        }
    }



}
