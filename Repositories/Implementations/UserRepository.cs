using Microsoft.EntityFrameworkCore;
using PharmacyAIS.Models;
using PharmacyAIS.Repositories.Interfaces;
using PharmacyAIS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PharmacyAIS.Repositories.Implementations
{
    public class UserRepository:IUserRepository
    {
        private readonly DataBaseContext _db;
        public UserRepository(DataBaseContext dataBaseContext)
        {
           _db = dataBaseContext;
        }

        public async Task<List<User>> GetUsers()
        {
            return await _db.User
                        .Include(p => p.Employee).ThenInclude(x => x.Department)
                        .Include(p => p.Role)
                        .ToListAsync();
        }

        public async Task<User?> Login(string name, string password)
        {
            return await _db.User.FirstOrDefaultAsync(x => x.Username == name && x.Password == password);  
        }
    }
}
