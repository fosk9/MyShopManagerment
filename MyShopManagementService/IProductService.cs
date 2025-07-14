using MyShopManagementBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementService
{
    public interface IProductService
    {
        public List<Product> GetAll();
        public List<Category> GetCategories();
        public Product Get(int id);
        public void Create(Product entity);
        public void Update(Product entity);
        public void Delete(int id);
        public bool Exist(int id);
    }
}
