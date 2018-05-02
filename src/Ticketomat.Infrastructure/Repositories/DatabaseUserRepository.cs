using System;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Ticketomat.Core.Domain;
using Ticketomat.Core.Repositories;

namespace Ticketomat.Infrastructure.Repositories
{
    public class DatabaseUserRepository : BaseDatabaseRepository, IUserRepository
    {
        public async Task AddAsync(User user)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Id", user.Id);  
                parameters.Add("@Role", user.Role);  
                parameters.Add("@Name", user.Name);  
                parameters.Add("@Email", user.Email);  
                parameters.Add("@Password", user.Password);  
                parameters.Add("@CreateAt", user.CreateAt);  
                SqlMapper.Execute(con, "AddUser", param: parameters, commandType: CommandType.StoredProcedure);  
                await Task.CompletedTask;  
            } catch (Exception ex) {  
                throw ex;  
            }  
        }

        public async Task<User> GetAsync(Guid id)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Id", id);
                var result = SqlMapper.Query<User>(con, "GetUserById", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault(); 
                return await Task.FromResult(result);
            } catch (Exception ex) {  
                throw ex;  
            } 
        }

        public async Task<User> GetAsync(string email)
        {
            try {  
                DynamicParameters parameters = new DynamicParameters();  
                parameters.Add("@Email", email);
                var result = SqlMapper.Query<User>(con, "GetUserByEmail", param: parameters, commandType: CommandType.StoredProcedure).FirstOrDefault(); 
                return await Task.FromResult(result);
            } catch (Exception ex) {  
                throw ex;  
            } 
        }

        public Task UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public Task DeleteAsync(User user)
        {
            throw new NotImplementedException();
        }
    }
}