using Microsoft.EntityFrameworkCore;
using MyShopManagementBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementDAO
{
    public class UserDAO
    {
        private static UserDAO instance = null;
        private static readonly object instanceLock = new object();

        private UserDAO() { }
        public static UserDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new UserDAO();
                    }
                    return instance;
                }
            }
        }

        public List<User> GetAll()
        {
            var users = new List<User>();

            try
            {
                using (var context = new MyShopContext())
                {
                    users = context.Users.Include(item => item.Role).ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return users;
        }

        public List<Role> GetRoles()
        {
            var roles = new List<Role>();

            try
            {
                using (var context = new MyShopContext())
                {
                    roles = context.Roles.ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            return roles;
        }

        public User Get(string email)
        {
            User entity = null;
            try
            {
                using (var context = new MyShopContext())
                {
                    entity = context.Users.Include(item => item.Role).SingleOrDefault<User>(item => item.Email.ToLower() == email.ToLower());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            return entity;
        }

        public void Create(User entity)
        {
            try
            {
                if (Instance.Exist(entity.Email))
                {
                    throw new Exception("Duplicated entity (id).");
                }
                using (var context = new MyShopContext())
                {
                    context.Users.Add(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void Update(User entity)
        {
            try
            {
                if (Instance.Exist(entity.Email) == false)
                {
                    throw new Exception("The entity does not exist: " + entity.Email);
                }
                using (var context = new MyShopContext())
                {

                    context.Entry<User>(entity).State = EntityState.Modified;
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public void Delete(string email)
        {
            try
            {
                using (var context = new MyShopContext())
                {
                    var entity = context.Users.SingleOrDefault<User>(item => item.Email.ToLower() == email.ToLower());
                    if (entity == null)
                    {
                        throw new Exception("Entity is not exist.");
                    }

                    context.Users.Remove(entity);
                    context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public bool Exist(string email)
        {
            User entity = null;
            try
            {
                using (var context = new MyShopContext())
                {
                    entity = context.Users.SingleOrDefault<User>(item => item.Email.ToLower() == email.ToLower());
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
