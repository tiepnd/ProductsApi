using ProductsApi.Models.v2;
using ProductsApi.Services.v2;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace ProductsApi.Controllers.v2
{
    [Route("api/v2/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<ActionResult<List<Product1>>> Get()
        {
            try
            {
                return await _productService.ReadProductsAsync();
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
            
        }

        [HttpGet("{id}", Name = "GetProductV2")]
        public ActionResult<Product1> Get(string id)
        {
            
            try
            {
                var product = _productService.ReadProductById(int.Parse(id));
                if (product == null)
                {
                    return NoContent();
                }
                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }

        [HttpPost]
        public async Task<ActionResult<Product1>> Create(Product1 product)
        {
            try
            {
                await _productService.Create(product);
                return CreatedAtRoute("GetProductV2", new { id = product.Id.ToString() }, product);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
           
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<Product1>> Update(string id, Product1 productIn)
        {

            try
            {
                Product1 product = await _productService.Update(int.Parse(id), productIn);

                if (product == null)
                {
                    return NotFound();
                }

                return product;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            
            try
            {
                await _productService.Delete(int.Parse(id));
                return NoContent();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return NoContent();
            }
        }
    }
}
