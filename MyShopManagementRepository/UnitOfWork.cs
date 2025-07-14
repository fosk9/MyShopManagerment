using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyShopManagementBO;

namespace MyShopManagementRepository
{
    public class UnitOfWork : IUnitOfWork
    {
        private bool _disposed = false;
        private readonly MyShopContext _context;
        private IUserRepository _userRepository;
        public UnitOfWork(MyShopContext context = null)
        {
            _context = context ?? new MyShopContext(new Microsoft.EntityFrameworkCore.DbContextOptions<MyShopContext>());
        }

        public IUserRepository UserRepository => _userRepository ??= new UserRepository(_context);

        public void SaveChange()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            _disposed = true;
        }
    }
}
