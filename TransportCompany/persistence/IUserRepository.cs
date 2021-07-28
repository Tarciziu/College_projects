using domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace persistence
{
    public interface IUserRepository :IRepository<User>
    {
        User findByUsername(String username);
    }
}
