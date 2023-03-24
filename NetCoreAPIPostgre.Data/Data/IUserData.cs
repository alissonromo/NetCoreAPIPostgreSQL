using NetCoreAPIPostgre.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgre.Data.Data
{
    public interface IUserData
    {
        Task<IEnumerable<User>> GetAllUser();
        Task<User> GetUserDetails(int id);
        Task<bool> InsertUser(User user);
        Task<bool> UpdateUser(User user);
        Task<bool> DeleteUser(User user);

    }
}
