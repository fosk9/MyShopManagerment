using MyShopManagementBO;
using MyShopManagementDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementRepository
{
    public class ProductRepository : IProductRepository
    {
        public List<Product> GetAll() => ProductDAO.Instance.GetAll();
        public List<Category> GetCategories() => ProductDAO.Instance.GetCategories();
        public Product Get(int id) => ProductDAO.Instance.Get(id);
        public void Create(Product entity) => ProductDAO.Instance.Create(entity);
        public void Update(Product entity) => ProductDAO.Instance.Update(entity);
        public void Delete(int id) => ProductDAO.Instance.Delete(id);
        public bool Exist(int id) => ProductDAO.Instance.Exist(id);
    }
}
