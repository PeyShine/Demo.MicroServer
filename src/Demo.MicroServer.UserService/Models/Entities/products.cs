using System;
using System.ComponentModel.DataAnnotations;

namespace Demo.MicroServer.UserService.Models
{
    /// <summary>
    /// 商品实体
    /// </summary>
    public class products
    {
        /// <summary>
        /// 商品id
        /// </summary>
        [Key]
        public string product_id { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public string product_name { get; set; }
        /// <summary>
        /// 商品价格
        /// </summary>
        public decimal product_price { get; set; }
        /// <summary>
        /// 商品描述
        /// </summary>
        public string product_description { get; set; }
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
