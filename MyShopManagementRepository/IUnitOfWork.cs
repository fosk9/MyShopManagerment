using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyShopManagementRepository
{
    public interface IUnitOfWork : IDisposable { 
        IUserRepository UserRepository { get; } 
        void SaveChange(); 
    }
}
