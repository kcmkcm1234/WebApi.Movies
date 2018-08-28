namespace WebApi.Movie.Service.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using WebApi.Movie.Service.Filters;

    [ServiceFilter(typeof(ExceptionFilter))]
    public class BaseController : Controller
    {
        public BaseController()
        {

        }
       
    }
}
