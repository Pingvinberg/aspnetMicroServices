using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class DiscountController: ControllerBase
    {
        private readonly IDiscountRepository _repository;
        private readonly ILogger<DiscountController> _logger;

        public DiscountController(IDiscountRepository repository, ILogger<DiscountController> logger)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [HttpGet("{productName}", Name = "GetDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> GetDiscount(string productName)
        {
            var coupon = await _repository.GetDiscount(productName);
            return Ok(coupon);
        }

        [HttpGet]
        [ProducesResponseType(typeof(List<Coupon>), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<List<Coupon>>> GetDiscounts()
        {
            var products = await _repository.GetDiscounts();
            return Ok(products);
        }

        [HttpPost]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<ActionResult<Coupon>> CreateProduct([FromBody] Coupon coupon)
        {
            await _repository.CreateDiscount(coupon);

            return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
        }

        [HttpPut]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateProduct([FromBody] Coupon coupon)
        {
            return Ok(await _repository.UpdateDiscount(coupon));
        }

        [HttpDelete("{productName)}", Name = "DeleteDiscount")]
        [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteProductById(string productName)
        {
            return Ok(await _repository.RemoveDiscount(productName));
        }
    }
}
