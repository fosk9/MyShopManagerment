using MyShopManagementBO;
using MyShopManagementRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;
        public UserService()
        {
            _unitOfWork = new UnitOfWork();
        }

        public User Get(string email)
        {
            return _unitOfWork.UserRepository.Get(email); // Thay FindUserByName bằng Get
        }

        public List<User> GetAll()
        {
            return _unitOfWork.UserRepository.GetAll();
        }

        public List<Role> GetRoles()
        {
            return _unitOfWork.UserRepository.GetRoles();
        }

        public User GetUser(string email)
        {
            return _unitOfWork.UserRepository.Get(email);
        }

        public void Create(User entity)
        {
            _unitOfWork.UserRepository.Create(entity);
            _unitOfWork.SaveChange();
        }

        public void Update(User entity)
        {
            _unitOfWork.UserRepository.Update(entity);
            _unitOfWork.SaveChange();
        }

        public void Delete(string email)
        {
            _unitOfWork.UserRepository.Delete(email);
            _unitOfWork.SaveChange();
        }

        public bool Exist(string email)
        {
            return _unitOfWork.UserRepository.Exist(email);
        }
    }
}

