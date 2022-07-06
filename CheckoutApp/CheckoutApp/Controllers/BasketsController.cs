using Microsoft.AspNetCore.Mvc;

namespace CheckoutApp.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BasketsController : ControllerBase
    {

        [HttpGet("{id}")]
        public IActionResult GetBasket(string id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult CreateBasket()
        {
            return Ok();
        }

        [HttpPut("{id}/article-line")]
        public IActionResult AddArticleLineToBasket(string id)
        {
            return Ok();
        }

        [HttpPatch("{id}")]
        public IActionResult PayBasket(string id)
        {
            return Ok();
        }

    }
}
