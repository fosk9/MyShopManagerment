﻿using MyShopManagementBO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementService
{
    public interface IUserService
    {
        public List<User> GetAll();
        public List<Role> GetRoles();
        public User Get(string email);
        public void Create(User entity);
        public void Update(User entity);
        public void Delete(string email);
        public bool Exist(string email);
        public interface IUserService
        {
            User Get(string email);
        }
    }
}
