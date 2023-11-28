using Ateno.Domain.Entities;
using Ateno.Domain.Interfaces;
using Ateno.Infra.Data.Context;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ateno.Infra.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public bool CheckBlockedAccount(string userName)
        {
            try
            {
                return (_applicationDbContext.User.Where(x => x.Email == userName && x.DisabledAt != null).Count() > 0);
            }
            catch
            {
                return false;
            }
        }

        public User GetById(string id)
        {
            try
            {
                return _applicationDbContext.User.Where(x => x.Id == id).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public User GetByEmail(string userName)
        {
            try
            {
                return _applicationDbContext.User.Where(x => x.Email == userName).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }


        public string GetFirstName(string id)
        {
            try
            {
                return _applicationDbContext.User.Where(x => x.Id == id).Select(x => x.FirstName).FirstOrDefault();
            }
            catch
            {
                return null;
            }
        }

        public async Task<bool> Create(User user)
        {
            try
            {
                _applicationDbContext.User.Add(user);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> Update(User user)
        {
            try
            {
                _applicationDbContext.Update(user);
                await _applicationDbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
