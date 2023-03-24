using Dapper;
using Microsoft.VisualBasic;
using NetCoreAPIPostgre.Model;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetCoreAPIPostgre.Data.Data
{
    public class UserData : IUserData
    {
        private PostgreSQLConfiguration _connectionString;
        
        public UserData(PostgreSQLConfiguration connectionString) 
        {  
            _connectionString = connectionString; 
        }

        protected NpgsqlConnection dbConnection()
        {
            return new NpgsqlConnection(_connectionString.ConnectionString);
        }

        public async Task<bool> DeleteUser(User user)
        {
            var db = dbConnection(); // NuGet -> Dapper = Escribir consulta y se ejecuta de manera asincrónica.

            var sql = @"
                        DELETE
                        FROM public.""Users""
                        WHERE ""Users"".""userId"" = @UserId";

            var result = await db.ExecuteAsync(sql, new { user.UserId }); //userId = user.UserId
            return result > 0;

        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var db = dbConnection(); 

            var sql = @"
                        SELECT ""Users"".""userId"", ""Users"".""identityTypeId"", ""Users"".""identity"", ""Users"".""password"", ""Users"".""passwordReset"", ""Users"".email
                        FROM public.""Users"" ";

            return await db.QueryAsync <User>(sql, new { });
        }

        public async Task<User> GetUserDetails(int id)
        {
            var db = dbConnection(); 

            var sql = @"
                        SELECT ""Users"".""userId"", ""Users"".""identityTypeId"", ""Users"".""identity"", ""Users"".""password"", ""Users"".""passwordReset"", ""Users"".email
                        FROM public.""Users"" 
                            WHERE ""Users"".""userId"" = @UserId";

            return await db.QueryFirstOrDefaultAsync <User>(sql, new { UserId = id});
        }

        public async Task<bool> InsertUser(User user)
        {
            var db = dbConnection(); 

            var sql = @"
                        INSERT INTO public.""Users"" (""identityTypeId"", identity, password, ""passwordReset"", email)
                        VALUES (@IdentityTypeId, @Identity, @Password, @PasswordReset, @Email) ";

            var result = await db.ExecuteAsync (sql, new { user.IdentityTypeId, user.Identity, user.Password, user.PasswordReset, user.Email});
            return result > 0;
        }

        public async Task<bool> UpdateUser(User user)
        {
            var db = dbConnection();

            var sql = @"
                        UPDATE public.""Users"" 
                        SET ""identityTypeId"" = @IdentityTypeId, 
                            identity = @Identity,
                            password = @Password,
                            ""passwordReset"" = @PasswordReset,
                            email = @Email
                        WHERE ""Users"".""userId"" = @UserId";

            var result = await db.ExecuteAsync(sql, new { user.IdentityTypeId, user.Identity, user.Password, user.PasswordReset, user.Email, user.UserId });
            return result > 0;
        }
    }
}
