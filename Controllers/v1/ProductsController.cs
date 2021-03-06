using ProductsApi.Models;
using ProductsApi.Services.v1;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;


namespace ProductsApi.Controllers.v1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly ProductService _productService;

        public ProductsController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet("test")]
        public ActionResult<IEnumerable<Product>> GetAll()
        {
            return new[]
            {
            new Product { Name = "Ana" },
            new Product { Name = "Felipe" },
            new Product { Name = "Emillia" }
        };
        }

        [HttpGet]
        public ActionResult<List<Product>> Get() => _productService.Get();

        [HttpGet("{id:length(24)}", Name = "GetProduct")]
        public ActionResult<Product> Get(string id)
        {
            var product = _productService.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return product;
        }

        [HttpPost]
        public ActionResult<Product> Create(Product product)
        {
            _productService.Create(product);
            return CreatedAtRoute("GetProduct", new { id = product.Id.ToString() }, product);
        }

        [HttpPut("{id:length(24)}")]
        public IActionResult Update(string id, Product productIn)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Update(id, productIn);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public IActionResult Delete(string id)
        {
            var product = _productService.Get(id);

            if (product == null)
            {
                return NotFound();
            }

            _productService.Remove(product.Id);

            return NoContent();
        }
    }

}
