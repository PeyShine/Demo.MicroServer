using Demo.MicroServer.UserService.Models;
using Demo.MicroServer.UserService.Repositories;
using System.Threading.Tasks;

namespace Demo.MicroServer.UserService.Services
{
    public class UserServices : IUserServices
    {
        private readonly IDemoMicroServerRepository<users> _repository_user;
        public UserServices(IDemoMicroServerRepository<users> repository_user)
        {
            _repository_user = repository_user;
        }

        public async Task<users> GetUserByIdAsync(string user_id)
        {
            return await _repository_user.GetByIdAsync(user_id);
        }

        public async Task<users> AddUserAsync(users user)
        {
            return await _repository_user.AddAsync(user);
        }

    }
}
