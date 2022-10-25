using Microsoft.AspNetCore.Mvc;

namespace TopMobil_API.Controllers
{
    [ApiController]
    [Route("/")]
    public class HomeController: ControllerBase
    {
        [HttpGet]
        public ActionResult Inicio()
        {
            return new ContentResult
            {
                ContentType = "text/html",
                Content = "<h1>API</h1>"
            };
}
    }
}