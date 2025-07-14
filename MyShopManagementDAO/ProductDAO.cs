using Microsoft.EntityFrameworkCore;
using MyShopManagementBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementDAO
{
    public class ProductDAO
    {
        private static ProductDAO instance = null;
        private static readonly object instanceLock = new object();

        private ProductDAO() { }
        public static ProductDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new ProductDAO();
                    }
                    return instance;
                }
            }
        }

        public List<Product> GetAll()
        {
            var products = new List<Product>();

            try
            {
                using (var context = new MyShopContext())
                {
                    products = context.Products.Include(item => item.Category).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return products;
        }

        public List<Category> GetCategories()
        {
            var categories = new List<Category>();

            try
            {
                using (var context = new MyShopContext())
                {
                    categories = context.Categories.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return categories;
        }

        public Product Get(int id)
        {
            Product entity = null;
            try
            {
                using (var context = new MyShopContext())
                {
                    entity = context.Products.Include(item => item.Category).SingleOrDefault<Product>(item => item.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public void Create(Product entity)
        {
            try
            {
                if (Instance.Exist(entity.Id))
                {
                    throw new Exception("Duplicated entity (id).");
                }
                using (var context = new MyShopContext())
                {
                    context.Products.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(Product entity)
        {
            try
            {
                if (Instance.Exist(entity.Id) == false)
                {
                    throw new Exception("The entity does not exist: " + entity.Id);
                }
                using (var context = new MyShopContext())
                {

                    context.Entry<Product>(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(int id)
        {
            try
            {
                using (var context = new MyShopContext())
                {
                    var entity = context.Products.SingleOrDefault<Product>(item => item.Id == id);
                    if (entity == null)
                    {
                        throw new Exception("Entity is not exist.");
                    }

                    context.Products.Remove(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Exist(int id)
        {
            Product entity = null;
            try
            {
                using (var context = new MyShopContext())
                {
                    entity = context.Products.SingleOrDefault<Product>(item => item.Id == id);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity != null;
        }
    }
}
