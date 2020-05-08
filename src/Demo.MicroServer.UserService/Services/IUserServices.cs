using Demo.MicroServer.UserService.Models;
using System.Threading.Tasks;

namespace Demo.MicroServer.UserService.Services
{
    /// <summary>
    /// 用户相关接口服务
    /// </summary>
    public interface IUserServices
    {
        /// <summary>
        /// 根据用户id获取用户信息
        /// </summary>
        /// <param name="user_id"></param>
        /// <returns></returns>
        Task<users> GetUserByIdAsync(string user_id);

        /// <summary>
        /// 添加用户
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        Task<users> AddUserAsync(users user);
    }
}
