using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgre.Model
{
    public class User
    {

        public int UserId { get; set; }

        public int IdentityTypeId { get; set; }

        public string Identity { get; set; }

        public string Password { get; set; }

        public string PasswordReset { get; set; }

        public string Email { get; set; }

    }
}
