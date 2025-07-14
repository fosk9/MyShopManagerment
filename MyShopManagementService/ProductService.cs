using MyShopManagementBO;
using MyShopManagementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementService
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService()
        {
            _productRepository = new ProductRepository();
        }

        public List<Product> GetAll() => _productRepository.GetAll();
        public List<Category> GetCategories() => _productRepository.GetCategories(); 
        public Product Get(int id) => _productRepository.Get(id);
        public void Create(Product entity) => _productRepository.Create(entity);
        public void Update(Product entity) => _productRepository.Update(entity);
        public void Delete(int id) => _productRepository.Delete(id);
        public bool Exist(int id) => _productRepository.Exist(id);
    }
}
