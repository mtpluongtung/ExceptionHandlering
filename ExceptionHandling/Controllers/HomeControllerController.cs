using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace ExceptionHandling.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeControllerController : ControllerBase
    {
        private readonly IExcetionHanler _excetionHanler;
        public HomeControllerController(IExcetionHanler excetionHanler) 
        { 
            _excetionHanler = excetionHanler;
        }
        [HttpGet]
        public async Task<IActionResult> TestExcetionHandler()
        {
            var res= _excetionHanler.TestExcetionHanler();
            return Ok(res);
        }
    }
}
