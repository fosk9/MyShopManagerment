using MyShopManagementBO;
using MyShopManagementDAO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementRepository
{
    public class UserRepository : IUserRepository
    {
        //public List<User> GetAll() => UserDAO.Instance.GetAll();
        //public List<Role> GetRoles() => UserDAO.Instance.GetRoles();
        //public User Get(string email) => UserDAO.Instance.Get(email);
        //public void Create(User entity) => UserDAO.Instance.Create(entity);
        //public void Update(User entity) => UserDAO.Instance.Update(entity);
        //public void Delete(string email) => UserDAO.Instance.Delete(email);
        //public bool Exist(string email) => UserDAO.Instance.Exist(email);
        private readonly MyShopContext _context;
        public UserRepository(MyShopContext context)
        {
            _context = context;
        }

        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public User Get(string email)
        {
            return _context.Users.FirstOrDefault(u => u.Email == email);
        }

        public void Create(User entity)
        {
            _context.Users.Add(entity);
        }

        public void Update(User entity)
        {
            _context.Users.Update(entity);
        }

        public void Delete(string email)
        {
            var user = _context.Users.FirstOrDefault(u => u.Email == email);
            if (user != null)
            {
                _context.Users.Remove(user);
            }
        }

        public bool Exist(string email)
        {
            return _context.Users.Any(u => u.Email == email);
        }
    }
}

