using Microsoft.AspNetCore.Mvc;

namespace CRUDOperation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CustomerReviewController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return Ok("Customer Review Controller working fine! ");         
        }
    }
}
