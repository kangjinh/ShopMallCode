using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NH.Entity.Model
{
    /// <summary>
    /// 商品表
    /// </summary>
    public class Goods
    {
        public virtual int ID { get; set; }
        /// <summary>
        /// 商品名称
        /// </summary>
        public virtual string GoodsName { get; set; }
        /// <summary>
        /// 商品编码
        /// </summary>
        public virtual string GoodsCode { get; set; }
        /// <summary>
        /// 子标题
        /// </summary>
        public virtual string SubTitle { get; set; }
        /// <summary>
        /// 分类ID
        /// </summary>
        public virtual int CategoryID { get; set; }
        /// <summary>
        /// 卖家ID
        /// </summary>
        public virtual int SalesID { get; set; }
        /// <summary>
        /// 总销量
        /// </summary>
        public virtual int TotalSaleCount { get; set; }
        /// <summary>
        /// 总库存
        /// </summary>
        public virtual int TotalCount { get; set; }
        /// <summary>
        /// 商品类型：0，普通商品；1，团购；2，特卖；3，展位
        /// </summary>
        public virtual int GoodsType { get; set; }
        /// <summary>
        /// 商品最低价格（以分为单位用于推荐）
        /// </summary>
        public virtual int ActualPrice { get; set; }
        /// <summary>
        /// 是否锁定
        /// </summary>
        public virtual bool IsLocked { get; set; }
        /// <summary>
        /// 创建时间
        /// </summary>
        public virtual DateTime CreateTime { get; set; }
        /// <summary>
        /// 更新时间
        /// </summary>
        public virtual DateTime UpdateTime { get; set; }
        /// <summary>
        /// 产地
        /// </summary>
        public virtual int PlaceID { get; set; }
        /// <summary>
        /// 品牌ID
        /// </summary>
        public virtual int BrandID { get; set; }


        public Goods()
        {
            this.ID = 0;
            this.BrandID = 0;
            this.PlaceID = 0;
            this.UpdateTime = DateTime.Now;
            this.CreateTime = DateTime.Now;
            this.IsLocked = false;
            this.GoodsType = 0;
            this.TotalCount = 0;
            this.TotalSaleCount = 0;
            this.SalesID = 0;
            this.CategoryID = 0;
            this.GoodsCode = "";
            this.GoodsName = "";
            this.ActualPrice = 0;
        }
    }
}
