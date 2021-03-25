using Microsoft.EntityFrameworkCore;
using ProductsApi.Models.v2;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;

namespace ProductsApi.Services.v2
{
    public class ProductService : IProductService
    {

        public readonly ProductsContext _products;

        public ProductService(ProductsContext products)
        {
            _products = products;
        }


        public async Task<List<Product1>> ReadProductsAsync()
        {
            try
            {
                var products = await _products.products1.ToListAsync();
                return products;
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }

        }
        public Product1 ReadProductById(int id)
        {
            return _products.products1.Find(id);
        }
        public async Task<Product1> Create(Product1 product1)
        {
            try
            {
                await _products.AddAsync(product1);
                await _products.SaveChangesAsync();
                return product1;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task<Product1> Update(int id, Product1 product1)
        {
            Console.WriteLine(product1.Name);
            try
            {

                var product = _products.products1.Find(id);
                if (product != null)
                {
                    product.Id = id;

                    product.Name = product1.Name;

                    product.CreatedDate = product1.CreatedDate;

                    product.ModifiedDate = product1.ModifiedDate;

                    product.Price = product1.Price;

                    await _products.SaveChangesAsync();
                    return product;
                }
                return null;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }
        public async Task Delete(int id)
        {
            try
            {
                var product = _products.products1.Find(id);
                if (product != null)
                {
                    _products.Remove(product);
                    await _products.SaveChangesAsync();
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine(ex.Message);
                throw;
            }
            catch (DbUpdateException e)
            {
                Console.WriteLine(e.Message);
                throw;
            }
        }

    }
}
