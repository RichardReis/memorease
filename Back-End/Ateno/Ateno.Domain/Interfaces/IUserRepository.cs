using Ateno.Domain.Entities;
using System.Threading.Tasks;

namespace Ateno.Domain.Interfaces
{
    public interface IUserRepository
    {
        bool CheckBlockedAccount(string userName);
        User GetById(string id);
        User GetByEmail(string userName);
        string GetFirstName(string id);
        Task<bool> Create(User user);
        Task<bool> Update(User user);
    }
}
