using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.MicroServer.UserService.Models
{
    /// <summary>
    /// 用户实体
    /// </summary>
    public class users
    {
        /// <summary>
        /// 用户id
        /// </summary>
        [Key]
        public string user_id { get; set; }
        /// <summary>
        /// 用户名
        /// </summary>
        public string user_name { get; set; }
        /// <summary>
        /// 账户密码
        /// </summary>
        public string pass_word { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public DateTime add_time { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime update_time { get; set; }
    }
}
