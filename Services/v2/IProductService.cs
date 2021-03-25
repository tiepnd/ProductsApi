using System;
using System.Collections.Generic;
using ProductsApi.Models.v2;
using System.Linq;
using System.Threading.Tasks;

namespace ProductsApi.Services.v2
{
    public interface IProductService
    {
        Task<List<Product1>> ReadProductsAsync();
        Product1 ReadProductById(int id);
        Task<Product1> Create(Product1 product1);
        Task<Product1> Update(int id, Product1 product1);
        Task Delete(int id);
    }
}
