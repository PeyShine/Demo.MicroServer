using IdentityServer4.Models;
using IdentityServer4.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Demo.MicroServer.IdentityServer4
{
    /// <summary>
    /// 获取用户信息并返回给客户端
    /// </summary>
    public class ProfileService: IProfileService
    {
        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            try
            {
                //用户信息
                var claims = context.Subject.Claims.ToList();

                //获取用户信息
                context.IssuedClaims = claims.ToList();
            }
            catch (Exception ex)
            {
                //log your error
            }
        }
        /// <summary>
        /// 获取或设置一个值，该值指示主题是否处于活动状态并且可以接收令牌。
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public async Task IsActiveAsync(IsActiveContext context)
        {
            context.IsActive = true;
        }
    }
}
